using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
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
        private readonly NuGetFramework currentFramework;

        public NugetPackageManager()
        {
            var frameworkName = Assembly.GetExecutingAssembly().GetCustomAttributes(true)
                .OfType<System.Runtime.Versioning.TargetFrameworkAttribute>()
                .Select(x => x.FrameworkName)
                .FirstOrDefault();

            if (frameworkName == null)
                currentFramework = NuGetFramework.AnyFramework;
            else
                currentFramework = NuGetFramework.ParseFrameworkName(frameworkName, new DefaultFrameworkNameProvider());
        }

        public async Task<Assembly> LoadNugetPackageAsync(AssemblyContext context, byte[] nupkgData)
        {
            using var stream = new MemoryStream(nupkgData);
            var reader = new PackageArchiveReader(stream, false);

            var identity = await reader.GetIdentityAsync(CancellationToken.None);

            var libItems = reader.GetLibItems();

            await GetDependenciesAsync(nupkgData);


            foreach (var file in libItems.Where(x => x.TargetFramework.Equals(currentFramework)))
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

            IEnumerable<PackageDependency> packages = groups.First(g => g.TargetFramework == currentFramework).Packages;
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
