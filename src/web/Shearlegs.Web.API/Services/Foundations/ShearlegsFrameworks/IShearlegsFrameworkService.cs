using Shearlegs.Web.API.Models.ShearlegsFrameworks.Params;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Results;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.ShearlegsFrameworks
{
    public interface IShearlegsFrameworkService
    {
        ValueTask<ShearlegsPluginResult> ExecuteShearlegsPluginAsync(ExecuteShearlegsPluginParams @params);
    }
}