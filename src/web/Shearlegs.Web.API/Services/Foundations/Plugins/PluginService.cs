using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.Plugins.Exceptions;
using Shearlegs.Web.API.Models.Plugins.Params;
using Shearlegs.Web.API.Models.Plugins.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
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

            Plugin plugin = await storageBroker.GetPluginAsync(@params);

            if (plugin == null)
            {
                throw new NotFoundPluginException();
            }

            return plugin;
        }

        public async ValueTask<Plugin> RetrievePluginByPackageId(string packageId)
        {
            GetPluginsParams @params = new()
            {
                PackageId = packageId
            };

            Plugin plugin = await storageBroker.GetPluginAsync(@params);

            if (plugin == null)
            {
                throw new NotFoundPluginException();
            }

            return plugin;
        }

        public async ValueTask<Plugin> AddPluginAsync(AddPluginParams @params)
        {
            AddPluginResult result = await storageBroker.AddPluginAsync(@params);

            if (result.StoredProcedureResult.ReturnValue == 1)
            {
                throw new AlreadyExistsPluginException();
            }

            return await RetrievePluginByIdAsync(result.PluginId.GetValueOrDefault());
        }

        public async ValueTask<Plugin> UpdatePluginAsync(UpdatePluginParams @params)
        {
            StoredProcedureResult result = await storageBroker.UpdatePluginAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundPluginException();
            }

            return await RetrievePluginByIdAsync(@params.PluginId);
        }
    }
}