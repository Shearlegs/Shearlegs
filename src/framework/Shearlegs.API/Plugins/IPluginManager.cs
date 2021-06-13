using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Parameters;
using Shearlegs.API.Plugins.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins
{
    public interface IPluginManager
    {
        Task<IPluginResult> ExecutePluginAsync(byte[] pluginData, string parametersJson);
        Task<IPluginInfo> GetPluginInfoAsync(byte[] pluginData);
    }
}
