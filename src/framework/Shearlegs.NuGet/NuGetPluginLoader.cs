using Microsoft.Extensions.Logging;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Resolver;
using Shearlegs.API.AssemblyLoading;
using Shearlegs.API.Plugins.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shearlegs.NuGet
{
    public class NuGetPluginLoader : IPluginLoader
    {
        public const string FrameworkName = "net5.0";

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

        public async Task<Assembly> LoadPluginAsync(IAssemblyContext context, Stream pluginStream)
        {
            using PackageArchiveReader reader = new PackageArchiveReader(pluginStream);

            IEnumerable<PackageDependencyGroup> groups = await reader.GetPackageDependenciesAsync(CancellationToken.None);

            NuGetFramework framework = frameworkReducer.GetNearest(targetFramework, groups.Select(g => g.TargetFramework));

            List<PackageDependency> dependencies = new List<PackageDependency>();
            foreach (PackageDependencyGroup group in groups)
            {
                if (group.TargetFramework == framework)
                {
                    dependencies.AddRange(group.Packages);
                    break;
                }
            }

            // TODO: Install dependencies
            IEnumerable<PackageIdentity> identities = await manager.InstallDependenciesAsync(dependencies);

            // Load dependencies
            foreach (PackageIdentity identity in identities)
            {
                string path = manager.GetNugetPackageFile(identity);
                using PackageArchiveReader dependencyReader = new PackageArchiveReader(path);
                await LoadLibAsync(context, dependencyReader);
            }

            // Load plugin 
            IEnumerable<Assembly> assemblies = await LoadLibAsync(context, reader);
            return assemblies.FirstOrDefault();
        }

        private async Task<IEnumerable<Assembly>> LoadLibAsync(IAssemblyContext context, PackageArchiveReader reader)
        {
            IEnumerable<FrameworkSpecificGroup> libs = await reader.GetLibItemsAsync(CancellationToken.None);

            List<Assembly> assemblies = new List<Assembly>();
            NuGetFramework framework = frameworkReducer.GetNearest(targetFramework, libs.Select(l => l.TargetFramework));
            foreach (FrameworkSpecificGroup lib in libs)
            {
                if (lib.TargetFramework != framework)
                {
                    continue;
                }

                foreach (string item in lib.Items)
                {
                    if (!item.EndsWith(".dll"))
                        continue;

                    ZipArchiveEntry entry = reader.GetEntry(item);
                    using Stream libStream = entry.Open();                    
                    using MemoryStream ms = new MemoryStream();
                    await libStream.CopyToAsync(ms);
                    ms.Position = 0;
                    Assembly assembly = context.LoadAssembly(ms);
                    assemblies.Add(assembly);
                    logger.LogInformation($"{assembly.GetName().Name} {assembly.GetName().Version} has been loaded!");
                }
            }
            return assemblies;
        }
    }
}
