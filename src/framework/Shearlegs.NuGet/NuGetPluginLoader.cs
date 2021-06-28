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

            // TODO: Install dependencies
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


            IEnumerable<FrameworkSpecificGroup> contentGroups = await reader.GetContentItemsAsync(CancellationToken.None);
            NuGetFramework contentFramework = frameworkReducer.GetNearest(targetFramework, contentGroups.Select(g => g.TargetFramework));

            List<IContentFile> contentFiles = new List<IContentFile>();
            foreach (FrameworkSpecificGroup group in contentGroups)
            {
                if (group.TargetFramework != contentFramework)
                {
                    continue;
                }

                foreach (string item in group.Items)
                {
                    ZipArchiveEntry entry = reader.GetEntry(item);
                    using Stream libStream = entry.Open();
                    using MemoryStream ms = new MemoryStream();
                    await libStream.CopyToAsync(ms);
                    ms.Position = 0;
                    contentFiles.Add(new ContentFile(entry.Name, ms.ToArray()));
                }
            }

            IPluginAssembly pluginAssembly = new PluginAssembly()
            {
                PackageId = pluginIdentity.Id,
                Version = pluginIdentity.Version.ToString(),
                IsPrerelease = pluginIdentity.Version.IsPrerelease,
                Assembly = assemblies.FirstOrDefault()
            };

            IContentFileStore fileStore = new ContentFileStore(contentFiles);


            return new PluginLoadResult(pluginAssembly, fileStore);
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
        
        private async Task<IEnumerable<FrameworkSpecificGroup>> GetRuntimeGroupsAsync(PackageArchiveReader reader)
        {
            IEnumerable<string> files = await reader.GetFilesAsync(CancellationToken.None);

            List<string> runtimeFiles = files.Where(x => x.StartsWith("runtimes/", StringComparison.OrdinalIgnoreCase)).ToList();
            List<NuGetFramework> runtimeFrameworks = new();
            Dictionary<NuGetFramework, List<string>> groups = new(new NuGetFrameworkFullComparer());

            foreach (string file in runtimeFiles)
            {
                string[] parts = file.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                parts = parts.Reverse().ToArray();
                NuGetFramework parsedFramework = null;

                foreach (string part in parts)
                {
                    NuGetFramework tempFramework = NuGetFramework.ParseFolder(part);

                    if (tempFramework.IsSpecificFramework && tempFramework.IsPackageBased && !tempFramework.IsUnsupported)
                    {
                        parsedFramework = tempFramework;
                        break;
                    }
                }

                if (parsedFramework == null)
                    continue;

                List<string> items = null;
                if (!groups.TryGetValue(parsedFramework, out items))
                {
                    items = new List<string>();
                    groups.Add(parsedFramework, items);
                }

                items.Add(file);
            }

            return groups.Keys.OrderBy(e => e, new NuGetFrameworkSorter())
                .Select(framework => new FrameworkSpecificGroup(framework, groups[framework].OrderBy(e => e, StringComparer.OrdinalIgnoreCase)));
        }


        private async Task<IEnumerable<Assembly>> LoadLibAsync(IAssemblyContext context, PackageArchiveReader reader, string path)
        {
            RuntimeGraph graph = GetRuntimeGraph(Directory.GetCurrentDirectory());
            ManagedCodeConventions conv = new ManagedCodeConventions(graph);

            ContentItemCollection collection = new ContentItemCollection();

            List<Assembly> assemblies = new List<Assembly>();



            // TODO: find a nicer way to handle this

            NuGetFramework framework;


            IEnumerable<string> items = null;

            IEnumerable<FrameworkSpecificGroup> runtimeGroups = await GetRuntimeGroupsAsync(reader);
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

            items = group?.Items?.Select(x => x.Path) ?? null;

            if (items != null)
            {
                foreach (string item in items)
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
