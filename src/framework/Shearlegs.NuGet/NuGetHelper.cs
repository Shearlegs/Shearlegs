using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.RuntimeModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shearlegs.NuGet
{
    public class NuGetHelper
    {
        public static RuntimeGraph GetRuntimeGraph(string expandedPath)
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

        public static async Task<IEnumerable<FrameworkSpecificGroup>> GetRuntimeGroupsAsync(PackageArchiveReader reader)
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
    }
}
