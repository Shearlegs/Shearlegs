using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.Plugins.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Plugins
{
    public interface IPluginService
    {
        ValueTask<Plugin> AddPluginAsync(AddPluginParams @params);
        ValueTask<IEnumerable<Plugin>> RetrieveAllPluginsAsync();
        ValueTask<Plugin> RetrievePluginByIdAsync(int pluginId);
        ValueTask<Plugin> RetrievePluginByPackageId(string packageId);
        ValueTask<Plugin> UpdatePluginAsync(UpdatePluginParams @params);
    }
}