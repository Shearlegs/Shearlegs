﻿using Microsoft.Extensions.Logging;
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
using Shearlegs.Core.Plugins.Loaders;
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

        public async Task<IPluginAssembly> LoadPluginAsync(IAssemblyContext context, Stream pluginStream)
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
                string path = manager.GetNugetPackageFile(identity, out string installedPath);
                using PackageArchiveReader dependencyReader = new PackageArchiveReader(path);
                await LoadLibAsync(context, dependencyReader, installedPath);
            }

            // Load plugin 
            IEnumerable<Assembly> assemblies = await LoadLibAsync(context, reader, null);

            PackageIdentity pluginIdentity = await reader.GetIdentityAsync(CancellationToken.None);

            return new PluginAssembly()
            {
                PackageId = pluginIdentity.Id,
                Version = pluginIdentity.Version.ToString(),
                IsPrerelease = pluginIdentity.Version.IsPrerelease,
                Assembly = assemblies.FirstOrDefault()
            };
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

        private async Task<IEnumerable<Assembly>> LoadLibAsync(IAssemblyContext context, PackageArchiveReader reader, string path)
        {
            RuntimeGraph graph = GetRuntimeGraph(Directory.GetCurrentDirectory());
            ManagedCodeConventions conv = new ManagedCodeConventions(graph);

            ContentItemCollection collection = new ContentItemCollection();
            collection.Load(await reader.GetFilesAsync(CancellationToken.None));

            List<Assembly> assemblies = new List<Assembly>();
            
            NuGetFramework framework = frameworkReducer.GetNearest(targetFramework, await reader.GetSupportedFrameworksAsync(CancellationToken.None));

            ContentItemGroup natives = collection.FindBestItemGroup(
                conv.Criteria.ForRuntime(RuntimeInformation.RuntimeIdentifier), 
                conv.Patterns.NativeLibraries);

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
