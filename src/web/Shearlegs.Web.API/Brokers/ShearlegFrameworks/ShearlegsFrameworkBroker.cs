using Microsoft.AspNetCore.Http;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins.Info;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Params;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Shearlegs
{
    public class ShearlegsFrameworkBroker : IShearlegsFrameworkBroker
    {
        private readonly IPluginManager pluginManager;

        public ShearlegsFrameworkBroker(IPluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
        }

        public async ValueTask<IPluginResult> ExecutePluginAsync(ExecuteShearlegsPluginParams @params)
        {
            IPluginResult pluginResult = await pluginManager.ExecutePluginAsync(@params.PluginData, @params.ParametersJson);

            return pluginResult;
        }

        public async ValueTask<IPluginInfo> GetPluginInfoAsync(GetShearlegsPluginInfoParams @params)
        {
            IPluginInfo pluginInfo = await pluginManager.GetPluginInfoAsync(@params.PluginData);

            return pluginInfo;
        }
    }
}
