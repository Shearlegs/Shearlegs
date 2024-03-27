using Microsoft.Extensions.Logging;
using NuGet.Client;
using NuGet.ContentModel;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.RuntimeModel;
using Shearlegs.API.AssemblyLoading;
using Shearlegs.API.Plugins.Content;
using Shearlegs.API.Plugins.Loaders;
using Shearlegs.Core.Plugins.Content;
using Shearlegs.Core.Plugins.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Shearlegs.NuGet
{
    public class NuGetPluginLoader : IPluginLoader
    {
        public const string FrameworkName = "net7.0";

        private readonly ILogger<NuGetPluginLoader> logger;

        private readonly NuGetPackageManager manager;

        private readonly NuGetFramework targetFramework;
        private readonly FrameworkReducer frameworkReducer;

        public NuGetPluginLoader(ILogger<NuGetPluginLoader> logger, NuGetPackageManager manager)
        {
            this.logger = logger;
            this.manager = manager;

            targetFramework = NuGetFramework.Parse(FrameworkName);
            frameworkReducer = new FrameworkReducer();
        }

        public async Task<IPluginLoadResult> LoadPluginAsync(IAssemblyContext context, Stream pluginStream)
        {
            using PackageArchiveReader reader = new PackageArchiveReader(pluginStream);

            IEnumerable<PackageDependencyGroup> dependencyGroups = await reader.GetPackageDependenciesAsync(CancellationToken.None);

            NuGetFramework dependencyFramework = frameworkReducer.GetNearest(targetFramework, dependencyGroups.Select(g => g.TargetFramework));

            List<PackageDependency> dependencies = new List<PackageDependency>();
            foreach (PackageDependencyGroup group in dependencyGroups)
            {
                if (group.TargetFramework == dependencyFramework)
                {
                    dependencies.AddRange(group.Packages);
                    break;
                }
            }

            // Install dependencies
            IEnumerable<PackageIdentity> identities = await manager.InstallDependenciesAsync(dependencies);

            // Load dependencies
            foreach (PackageIdentity identity in identities)
            {
                string path = manager.GetNugetPackageFile(identity, out string installedPath);
                using PackageArchiveReader dependencyReader = new PackageArchiveReader(path);
                await LoadLibAsync(context, dependencyReader, installedPath);
            }

            // Load plugin 
            IEnumerable<Assembly> assemblies = await LoadLibAsync(context, reader, null);

            PackageIdentity pluginIdentity = await reader.GetIdentityAsync(CancellationToken.None);

            IPluginAssembly pluginAssembly = new PluginAssembly()
            {
                PackageId = pluginIdentity.Id,
                Version = pluginIdentity.Version.ToString(),
                IsPrerelease = pluginIdentity.Version.IsPrerelease,
                Assembly = assemblies.FirstOrDefault()
            };
            
            return new PluginLoadResult(pluginAssembly);
        }

        private async Task<IEnumerable<Assembly>> LoadLibAsync(IAssemblyContext context, PackageArchiveReader reader, string path)
        {
            RuntimeGraph graph = NuGetHelper.GetRuntimeGraph(Directory.GetCurrentDirectory());
            ManagedCodeConventions conv = new(graph);
            ContentItemCollection collection = new();

            List<Assembly> assemblies = new();

            NuGetFramework framework;

            IEnumerable<FrameworkSpecificGroup> runtimeGroups = await NuGetHelper.GetRuntimeGroupsAsync(reader);
            if (runtimeGroups.Any())
            {
                framework = frameworkReducer.GetNearest(targetFramework, runtimeGroups.Select(x => x.TargetFramework));
                collection.Load(runtimeGroups.First(x => x.TargetFramework == framework).Items);
            } else
            {
                framework = frameworkReducer.GetNearest(targetFramework, await reader.GetSupportedFrameworksAsync(CancellationToken.None));
                collection.Load(await reader.GetFilesAsync(CancellationToken.None));
            }

            ContentItemGroup group = collection.FindBestItemGroup(
                  conv.Criteria.ForFrameworkAndRuntime(framework, RuntimeInformation.RuntimeIdentifier),
                  conv.Patterns.RuntimeAssemblies);

            if (group != null)
            {
                foreach (ContentItem item in group.Items)
                {
                    if (!item.Path.EndsWith(".dll"))
                        continue;

                    ZipArchiveEntry entry = reader.GetEntry(item.Path);
                    using Stream libStream = entry.Open();
                    using MemoryStream ms = new MemoryStream();
                    await libStream.CopyToAsync(ms);
                    ms.Position = 0;
                    Assembly assembly = context.LoadAssembly(ms);
                    assemblies.Add(assembly);
                    logger.LogInformation($"{assembly.GetName().Name} {assembly.GetName().Version} has been loaded!");
                }
            }

            ContentItemGroup natives = collection.FindBestItemGroup(
                conv.Criteria.ForRuntime(RuntimeInformation.RuntimeIdentifier),
                conv.Patterns.NativeLibraries);

            if (natives != null)
            {
                foreach (ContentItem item in natives.Items)
                {
                    if (!item.Path.EndsWith(".dll"))
                        continue;

                    string itemPath = Path.Combine(path, item.Path);
                    NativeLibrary.Load(itemPath);
                }
            }

            return assemblies;
        }
    }
}
