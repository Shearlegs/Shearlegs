using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Params;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Shearlegs
{
    public interface IShearlegsFrameworkBroker
    {
        ValueTask<IPluginResult> ExecutePluginAsync(ExecuteShearlegsPluginParams @params);
        ValueTask<IPluginInfo> GetPluginInfoAsync(GetShearlegsPluginInfoParams @params);
    }
}