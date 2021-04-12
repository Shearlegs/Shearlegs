using Shearlegs.API.Plugins.Result;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins
{
    public interface IPluginManager
    {
        Task<IPluginResult> ExecutePluginAsync(byte[] pluginData, string parametersJson);
    }
}
