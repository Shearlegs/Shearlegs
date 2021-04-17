using Microsoft.Extensions.DependencyModel;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Packaging.Signing;
using NuGet.Protocol.Core.Types;
using NuGet.Resolver;
using NuGet.Versioning;
using Shearlegs.API.Constants;
using Shearlegs.Core.AssemblyLoading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Shearlegs.Core.NuGet
{
    public class NugetPackageManager
    {
        private readonly NuGetFramework targetFramework;
        private readonly PackageSourceProvider sourceProvider;

        public NugetPackageManager()
        {
            var frameworkName = Assembly.GetExecutingAssembly().GetCustomAttributes(true)
                .OfType<System.Runtime.Versioning.TargetFrameworkAttribute>()
                .Select(x => x.FrameworkName)
                .FirstOrDefault();

            sourceProvider = new PackageSourceProvider(NullSettings.Instance, new[]
            {
                new PackageSource("https://api.nuget.org/v3/index.json", "NuGet.org")
            });

            if (frameworkName == null)
                targetFramework = NuGetFramework.AnyFramework;
            else
                targetFramework = NuGetFramework.ParseFrameworkName(frameworkName, new DefaultFrameworkNameProvider());
        }

        public async Task LoadPluginAsync(AssemblyContext context, byte[] nupkgData)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            MemoryStream stream = new(nupkgData);
            PackageArchiveReader reader = new(stream, false);

            PackageIdentity identity = await reader.GetIdentityAsync(CancellationToken.None);

            SourceRepositoryProvider sourceRepositoryProvider = new(sourceProvider, Repository.Provider.GetCoreV3());
            var sourceRepository = sourceRepositoryProvider.CreateRepository(sourceProvider.GetPackageSourceByName("NuGet.org"));

            // Disposable source cache.
            using var sourceCacheContext = new SourceCacheContext();

            // You should use an actual logger here, this is a NuGet ILogger instance.
            var logger = new NullLogger();

            // The framework we're using.
            var allPackages = new HashSet<SourcePackageDependencyInfo>();

            var dependencyContext = DependencyContext.Default;

            await GetPackageDependencies(identity, sourceCacheContext, targetFramework, logger, sourceRepository, dependencyContext, allPackages, cancellationToken);


            var packagesToInstall = GetPackagesToInstall(sourceRepositoryProvider, logger, identity, allPackages);

            var packageDirectory = Path.Combine(Environment.CurrentDirectory, DirectoryConstants.NugetPackagesDirectory);
            var nugetSettings = Settings.LoadDefaultSettings(packageDirectory);

            await InstallPackages(sourceCacheContext, logger, packagesToInstall, packageDirectory, nugetSettings, cancellationToken);

            foreach (var package in packagesToInstall)
            {

            }

        }

        private bool DependencySuppliedByHost(DependencyContext hostDependencies, PackageDependency dep)
        {
            if (RuntimeProvidedPackages.IsPackageProvidedByRuntime(dep.Id))
            {
                return true;
            }

            // See if a runtime library with the same ID as the package is available in the host's runtime libraries.
            var runtimeLib = hostDependencies.RuntimeLibraries.FirstOrDefault(r => r.Name == dep.Id);

            if (runtimeLib is object)
            {
                // What version of the library is the host using?
                var parsedLibVersion = NuGetVersion.Parse(runtimeLib.Version);

                if (parsedLibVersion.IsPrerelease)
                {
                    // Always use pre-release versions from the host, otherwise it becomes
                    // a nightmare to develop across multiple active versions.
                    return true;
                }
                else
                {
                    // Does the host version satisfy the version range of the requested package?
                    // If so, we can provide it; otherwise, we cannot.
                    return dep.VersionRange.Satisfies(parsedLibVersion);
                }
            }

            return false;
        }

        private async Task GetPackageDependencies(PackageIdentity package, SourceCacheContext cacheContext, NuGetFramework framework, ILogger logger, 
            SourceRepository sourceRepository, DependencyContext hostDependencies, ISet<SourcePackageDependencyInfo> availablePackages, CancellationToken cancelToken)
        {
            // Don't recurse over a package we've already seen.
            if (availablePackages.Contains(package))
            {
                return;
            }

            // Get the dependency info for the package.
            var dependencyInfoResource = await sourceRepository.GetResourceAsync<DependencyInfoResource>();
            var dependencyInfo = await dependencyInfoResource.ResolvePackage(
                package,
                framework,
                cacheContext,
                logger,
                cancelToken);

            // No info for the package in this repository.
            if (dependencyInfo == null)
            {
                return;
            }


            // Filter the dependency info.
            // Don't bring in any dependencies that are provided by the host.
            var actualSourceDep = new SourcePackageDependencyInfo(
                dependencyInfo.Id,
                dependencyInfo.Version,
                dependencyInfo.Dependencies.Where(dep => !DependencySuppliedByHost(hostDependencies, dep)),
                dependencyInfo.Listed,
                dependencyInfo.Source);

            availablePackages.Add(actualSourceDep);

            // Recurse through each package.
            foreach (var dependency in actualSourceDep.Dependencies)
            {
                await GetPackageDependencies(
                    new PackageIdentity(dependency.Id, dependency.VersionRange.MinVersion),
                    cacheContext,
                    framework,
                    logger,
                    sourceRepository,
                    hostDependencies,
                    availablePackages,
                    cancelToken);
            }
        }

        private IEnumerable<SourcePackageDependencyInfo> GetPackagesToInstall(SourceRepositoryProvider sourceRepositoryProvider, ILogger logger, PackageIdentity identity,
                                                                      HashSet<SourcePackageDependencyInfo> allPackages)
        {
            // Create a package resolver context.
            var resolverContext = new PackageResolverContext(
                    DependencyBehavior.Lowest,
                    new string[] { identity.Id },
                    Enumerable.Empty<string>(),
                    Enumerable.Empty<PackageReference>(),
                    Enumerable.Empty<PackageIdentity>(),
                    allPackages,
                    sourceRepositoryProvider.GetRepositories().Select(s => s.PackageSource),
                    logger);

            var resolver = new PackageResolver();

            // Work out the actual set of packages to install.
            var packagesToInstall = resolver.Resolve(resolverContext, CancellationToken.None)
                                            .Select(p => allPackages.Single(x => PackageIdentityComparer.Default.Equals(x, p)));
            return packagesToInstall;
        }


        private async Task InstallPackages(SourceCacheContext sourceCacheContext, ILogger logger,
                                    IEnumerable<SourcePackageDependencyInfo> packagesToInstall, string rootPackagesDirectory,
                                    ISettings nugetSettings, CancellationToken cancellationToken)
        {
            var packagePathResolver = new PackagePathResolver(rootPackagesDirectory, true);
            var packageExtractionContext = new PackageExtractionContext(
                PackageSaveMode.Defaultv3,
                XmlDocFileSaveMode.Skip,
                ClientPolicyContext.GetClientPolicy(nugetSettings, logger),
                logger);

            foreach (var package in packagesToInstall)
            {
                var downloadResource = await package.Source.GetResourceAsync<DownloadResource>(cancellationToken);

                // Download the package (might come from the shared package cache).
                var downloadResult = await downloadResource.GetDownloadResourceResultAsync(
                    package,
                    new PackageDownloadContext(sourceCacheContext),
                    SettingsUtility.GetGlobalPackagesFolder(nugetSettings),
                    logger,
                    cancellationToken);

                // Extract the package into the target directory.
                await PackageExtractor.ExtractPackageAsync(
                    downloadResult.PackageSource,
                    downloadResult.PackageStream,
                    packagePathResolver,
                    packageExtractionContext,
                    cancellationToken);
            }
        }

        public async Task<Assembly> LoadNugetPackageAsync(AssemblyContext context, byte[] nupkgData)
        {

            using var stream = new MemoryStream(nupkgData);
            var reader = new PackageArchiveReader(stream, false);

            var identity = await reader.GetIdentityAsync(CancellationToken.None);

            var libItems = reader.GetLibItems();

            await GetDependenciesAsync(nupkgData);


            foreach (var file in libItems.Where(x => x.TargetFramework.Equals(targetFramework)))
            {
                foreach (var item in file.Items)
                {
                    if (!item.Contains(".dll"))
                        continue;

                    var entry = reader.GetEntry(item);

                    using var entryStream = entry.Open();
                    byte[] buffer = new byte[entryStream.Length];
                    await entryStream.WriteAsync(buffer);

                    var ms = new MemoryStream();
                    await entryStream.CopyToAsync(entryStream);

                    return context.LoadAssembly(ms);
                }
            }
            return null;
        }

        private async Task<IEnumerable<PackageDependency>> GetDependenciesAsync(byte[] nupkgData)
        {
            using MemoryStream stream = new MemoryStream(nupkgData);
            using PackageArchiveReader reader = new PackageArchiveReader(stream);

            List<PackageDependency> list = new List<PackageDependency>();
            IEnumerable<PackageDependencyGroup> groups = await reader.GetPackageDependenciesAsync(CancellationToken.None);

            IEnumerable<PackageDependency> packages = groups.First(g => g.TargetFramework == targetFramework).Packages;
            list.AddRange(packages);

            foreach (PackageDependency dependency in list)
            {
                // await GetDependenciesAsync();
            }

            return list; 
        }

        private async Task InstallAsync(PackageIdentity packageIdentity)
        {

        }

        private async Task DownloadPackageAsync()
        {

        }
    }
}
