using Shearlegs.API.Plugins.Loaders;
using System.Reflection;

namespace Shearlegs.Core.Plugins.Loaders
{
    public class PluginAssembly : IPluginAssembly
    {
        public string PackageId { get; set; }
        public string Version { get; set; }
        public bool IsPrerelease { get; set; }
        public Assembly Assembly { get; set; }
    }
}
