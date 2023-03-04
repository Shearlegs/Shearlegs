using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.Plugins.Params;
using Shearlegs.Web.API.Models.Plugins.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<AddPluginResult> AddPluginAsync(AddPluginParams @params);
        ValueTask<Plugin> GetPluginAsync(GetPluginsParams @params);
        ValueTask<IEnumerable<Plugin>> GetPluginsAsync(GetPluginsParams @params);
    }
}
