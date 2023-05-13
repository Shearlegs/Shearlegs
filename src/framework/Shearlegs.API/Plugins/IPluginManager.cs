using Shearlegs.API.Plugins.Content;
using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Parameters;
using Shearlegs.API.Plugins.Result;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins
{
    public interface IPluginManager
    {
        IPlugin ActivatePlugin(Assembly assembly, string parametersJson);
        Task<IPluginResult> ExecutePluginAsync(byte[] pluginData, string parametersJson);
        Task<IPluginInfo> GetPluginInfoAsync(byte[] pluginData);
        Task<IPluginInfo> GetPluginInfoAsync(Stream stream);
    }
}
