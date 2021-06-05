using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NuGet.Client;
using NuGet.ContentModel;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Resolver;
using NuGet.RuntimeModel;
using Shearlegs.API.AssemblyLoading;
using Shearlegs.API.Plugins.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

        private RuntimeGraph GetRuntimeGraph(string expandedPath)
        {
            string runtimeGraphFile = Path.Combine(expandedPath, RuntimeGraph.RuntimeGraphFileName);
            if (File.Exists(runtimeGraphFile))
            {
                using (FileStream stream = File.OpenRead(runtimeGraphFile))
                {
                    return JsonRuntimeFormat.ReadRuntimeGraph(stream);
                }
            }

            return null;
        }

        private async Task<IEnumerable<Assembly>> LoadLibAsync(IAssemblyContext context, PackageArchiveReader reader)
        {
            RuntimeGraph graph = GetRuntimeGraph(Directory.GetCurrentDirectory());
            ManagedCodeConventions conv = new ManagedCodeConventions(graph);

            ContentItemCollection collection = new ContentItemCollection();
            collection.Load(await reader.GetFilesAsync(CancellationToken.None));

            List<Assembly> assemblies = new List<Assembly>();
            
            NuGetFramework framework = frameworkReducer.GetNearest(targetFramework, await reader.GetSupportedFrameworksAsync(CancellationToken.None));

            ContentItemGroup group = collection.FindBestItemGroup(
                conv.Criteria.ForFrameworkAndRuntime(framework, RuntimeInformation.RuntimeIdentifier),
                conv.Patterns.RuntimeAssemblies);

            if (group == null)
            {
                return assemblies;
            }

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

            return assemblies;
        }
    }
}
