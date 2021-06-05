using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
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
using Shearlegs.NuGet.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ILogger = NuGet.Common.ILogger;

namespace Shearlegs.NuGet
{
    public class NuGetPackageManager
    {
        public const string FrameworkName = "net5.0";

        private readonly ILogger<NuGetPackageManager> logger;

        private readonly string packageDirectory;

        private readonly ISettings settings;
        private readonly PackageSource packageSource;

        private readonly PackageSourceProvider sourceProvider;
        private readonly SourceRepositoryProvider sourceRepositoryProvider;

        private readonly NuGetFramework targetFramework;

        private readonly DependencyContext dependencyContext;

        private readonly PackagePathResolver packagePathResolver;

        private readonly ILogger nugetLogger;


        public NuGetPackageManager(ILogger<NuGetPackageManager> logger)
        {
            this.logger = logger;

            packageDirectory = Path.Combine(Environment.CurrentDirectory, DirectoryConstants.NugetPackagesDirectory);

            settings = Settings.LoadDefaultSettings(packageDirectory);
            packageSource = new("https://api.nuget.org/v3/index.json", "NuGet.org");

            sourceProvider = new PackageSourceProvider(settings, new PackageSource[]
            {
                packageSource
            });
            sourceRepositoryProvider = new SourceRepositoryProvider(sourceProvider, Repository.Provider.GetCoreV3());

            targetFramework = NuGetFramework.Parse(FrameworkName);

            dependencyContext = DependencyContext.Default;

            packagePathResolver = new PackagePathResolver(packageDirectory);

            nugetLogger = new NullLogger();
        }

        public async Task<IEnumerable<PackageIdentity>> InstallDependenciesAsync(List<PackageDependency> dependencies)
        {
            using SourceCacheContext sourceCacheContext = new SourceCacheContext();

            List<PackageIdentity> packageIdentities = new List<PackageIdentity>();
            HashSet<SourcePackageDependencyInfo> allPackages = new();

            List<PackageConfiguration> configs = new List<PackageConfiguration>();
            foreach (PackageDependency dependency in dependencies)
            {
                PackageConfiguration config = PackageConfiguration.FromPackageDependency(dependency);
                

                PackageIdentity packageIdentity = await GetPackageIdentityAsync(config, sourceCacheContext);
                if (packageIdentity is null)
                {
                    throw new InvalidOperationException($"Cannot find package {config.Package}.");
                }

                if (!DependencySuppliedByHost(dependency))
                {
                    configs.Add(config);
                    await GetPackageDependencies(packageIdentity, sourceCacheContext, allPackages);
                }                    
            }

            var packagesToInstall = GetPackagesToInstall(configs, allPackages);

            await InstallPackages(sourceCacheContext, packagesToInstall);
            
            packageIdentities.AddRange(packagesToInstall);

            return packageIdentities;
        }

        public string GetNugetPackageFile(PackageIdentity identity, out string installedPath)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            installedPath = packagePathResolver.GetInstallPath(identity);
            var dirName = new DirectoryInfo(installedPath).Name;

            return Path.Combine(installedPath, dirName + ".nupkg");
        }

        private async Task InstallPackages(SourceCacheContext sourceCacheContext, IEnumerable<SourcePackageDependencyInfo> packagesToInstall)
        {
            var packagePathResolver = new PackagePathResolver(packageDirectory, true);
            var packageExtractionContext = new PackageExtractionContext(
                PackageSaveMode.Defaultv3,
                XmlDocFileSaveMode.Skip,
                ClientPolicyContext.GetClientPolicy(settings, nugetLogger),
                nugetLogger);

            foreach (var package in packagesToInstall)
            {
                var downloadResource = await package.Source.GetResourceAsync<DownloadResource>(CancellationToken.None);

                // Download the package (might come from the shared package cache).
                var downloadResult = await downloadResource.GetDownloadResourceResultAsync(
                    package,
                    new PackageDownloadContext(sourceCacheContext),
                    SettingsUtility.GetGlobalPackagesFolder(settings),
                    nugetLogger,
                    CancellationToken.None);

                // Extract the package into the target directory.
                await PackageExtractor.ExtractPackageAsync(
                    downloadResult.PackageSource,
                    downloadResult.PackageStream,
                    packagePathResolver,
                    packageExtractionContext,
                    CancellationToken.None);
            }
        }

        private IEnumerable<SourcePackageDependencyInfo> GetPackagesToInstall(IEnumerable<PackageConfiguration> configs, HashSet<SourcePackageDependencyInfo> allPackages)
        {
            // Create a package resolver context.
            var resolverContext = new PackageResolverContext(
                    DependencyBehavior.Lowest,
                    configs.Select(x => x.Package),
                    Enumerable.Empty<string>(),
                    Enumerable.Empty<PackageReference>(),
                    Enumerable.Empty<PackageIdentity>(),
                    allPackages,
                    sourceRepositoryProvider.GetRepositories().Select(s => s.PackageSource),
                    nugetLogger);

            var resolver = new PackageResolver();

            // Work out the actual set of packages to install.
            var packagesToInstall = resolver.Resolve(resolverContext, CancellationToken.None).Select(p => allPackages.Single(x => PackageIdentityComparer.Default.Equals(x, p)));
            return packagesToInstall;
        }

        private bool DependencySuppliedByHost(PackageDependency dep)
        {
            // Check our look-up list.
            if (RuntimeProvidedPackages.IsPackageProvidedByRuntime(dep.Id))
            {
                return true;
            }

            // See if a runtime library with the same ID as the package is available in the host's runtime libraries.
            var runtimeLib = dependencyContext.RuntimeLibraries.FirstOrDefault(r => r.Name == dep.Id);

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

        private async Task GetPackageDependencies(PackageIdentity package, SourceCacheContext cache, ISet<SourcePackageDependencyInfo> availablePackages)
        {
            // Don't recurse over a package we've already seen.
            if (availablePackages.Contains(package))
            {
                return;
            }

            foreach (var sourceRepository in sourceRepositoryProvider.GetRepositories())
            {
                // Get the dependency info for the package.
                var dependencyInfoResource = await sourceRepository.GetResourceAsync<DependencyInfoResource>();
                var dependencyInfo = await dependencyInfoResource.ResolvePackage(package, targetFramework, cache, nugetLogger, CancellationToken.None);

                // No info for the package in this repository.
                if (dependencyInfo == null)
                {
                    continue;
                }

                // Filter the dependency info.
                // Don't bring in any dependencies that are provided by the host.
                var actualSourceDep = new SourcePackageDependencyInfo(
                    dependencyInfo.Id,
                    dependencyInfo.Version,
                    dependencyInfo.Dependencies.Where(dep => !DependencySuppliedByHost(dep)),
                    dependencyInfo.Listed,
                    dependencyInfo.Source);

                availablePackages.Add(actualSourceDep);

                // Recurse through each package.
                foreach (var dependency in actualSourceDep.Dependencies)
                {
                    await GetPackageDependencies(new PackageIdentity(dependency.Id, dependency.VersionRange.MinVersion), cache, availablePackages);
                }

                break;
            }
        }

        private async Task<PackageIdentity> GetPackageIdentityAsync(PackageConfiguration config, SourceCacheContext cache)
        {
            foreach (var sourceRepository in sourceRepositoryProvider.GetRepositories())
            {
                // Get a 'resource' from the repository.
                var findPackageResource = await sourceRepository.GetResourceAsync<FindPackageByIdResource>();

                // Get the list of all available versions of the package in the repository.
                var allVersions = await findPackageResource.GetAllVersionsAsync(config.Package, cache, nugetLogger, CancellationToken.None);

                NuGetVersion selected;

                // Have we specified a version range?
                if (config.VersionRange != null)
                {
                    // Find the best package version match for the range.
                    // Consider pre-release versions, but only if the extension is configured to use them.
                    var bestVersion = config.VersionRange.FindBestMatch(allVersions.Where(v => config.PreRelease || !v.IsPrerelease));

                    selected = bestVersion;
                }
                else
                {
                    // No version; choose the latest, allow pre-release if configured.
                    selected = allVersions.LastOrDefault(v => v.IsPrerelease == config.PreRelease);
                }

                if (selected is object)
                {
                    return new PackageIdentity(config.Package, selected);
                }
            }

            return null;
        }
    }
}
