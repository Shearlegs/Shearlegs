using Microsoft.Net.Http.Headers;
using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Web.API.Brokers.Shearlegs;
using Shearlegs.Web.API.Models.ShearlegsFrameworks;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Params;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Results;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.ShearlegsFrameworks
{
    public partial class ShearlegsFrameworkService : IShearlegsFrameworkService
    {
        private readonly IShearlegsFrameworkBroker shearlegsFrameworkBroker;

        public ShearlegsFrameworkService(IShearlegsFrameworkBroker shearlegsFrameworkBroker)
        {
            this.shearlegsFrameworkBroker = shearlegsFrameworkBroker;
        }

        public async ValueTask<ShearlegsPluginResult> ExecuteShearlegsPluginAsync(ExecuteShearlegsPluginParams @params)
        {
            IPluginResult pluginResult = await shearlegsFrameworkBroker.ExecutePluginAsync(@params);

            return MapPluginResultToShearlegsPluginResult(pluginResult);
        }

        public async ValueTask<ShearlegsPluginInfo> GetShearlegsPluginInfoAsync(GetShearlegsPluginInfoParams @params)
        {
            IPluginInfo pluginInfo = await shearlegsFrameworkBroker.GetPluginInfoAsync(@params);

            return MapPluginInfoToShearlegsPluginInfo(pluginInfo);
        }
    }
}
