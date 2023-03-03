using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.Plugins.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Plugins
{
    public class PluginService : IPluginService
    {
        private readonly IStorageBroker storageBroker;

        public PluginService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<IEnumerable<Plugin>> RetrieveAllPluginsAsync()
        {
            GetPluginsParams @params = new();

            return await storageBroker.GetPluginsAsync(@params);
        }

        public async ValueTask<Plugin> RetrievePluginByIdAsync(int pluginId)
        {
            GetPluginsParams @params = new()
            {
                PluginId = pluginId
            };

            return await storageBroker.GetPluginAsync(@params);
        }

        public async ValueTask<Plugin> RetrievePluginByPackageId(string packageId)
        {
            GetPluginsParams @params = new()
            {
                PackageId = packageId
            };

            return await storageBroker.GetPluginAsync(@params);
        }
    }
}
