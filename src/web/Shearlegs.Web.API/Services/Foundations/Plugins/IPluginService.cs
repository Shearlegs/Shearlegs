using Shearlegs.Web.API.Models.Plugins;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Plugins
{
    public interface IPluginService
    {
        ValueTask<IEnumerable<Plugin>> RetrieveAllPluginsAsync();
        ValueTask<Plugin> RetrievePluginByPackageId(string packageId);
    }
}