using Shearlegs.API.Plugins.Content;
using Shearlegs.API.Plugins.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins.Loaders
{
    public class PluginLoadResult : IPluginLoadResult
    {
        public PluginLoadResult(IPluginAssembly pluginAssembly)
        {
            PluginAssembly = pluginAssembly;
        }

        public IPluginAssembly PluginAssembly { get; }
    }
}
