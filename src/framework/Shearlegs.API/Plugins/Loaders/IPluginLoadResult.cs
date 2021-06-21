using Shearlegs.API.Plugins.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins.Loaders
{
    public interface IPluginLoadResult
    {
        IPluginAssembly PluginAssembly { get; }
        IContentFileStore FileStore { get; }
    }
}
