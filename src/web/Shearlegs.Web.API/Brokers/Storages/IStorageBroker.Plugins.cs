using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.Plugins.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Plugin> GetPluginAsync(GetPluginsParams @params);
        ValueTask<IEnumerable<Plugin>> GetPluginsAsync(GetPluginsParams @params);
    }
}
