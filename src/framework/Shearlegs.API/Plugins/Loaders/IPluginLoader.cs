using Shearlegs.API.AssemblyLoading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins.Loaders
{
    public interface IPluginLoader
    {
        Task<Assembly> LoadPluginAsync(IAssemblyContext context, Stream pluginStream);
    }
}
