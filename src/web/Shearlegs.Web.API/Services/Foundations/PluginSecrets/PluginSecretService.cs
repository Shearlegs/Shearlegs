using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Models.Plugins.Exceptions;
using Shearlegs.Web.API.Models.PluginSecrets;
using Shearlegs.Web.API.Models.PluginSecrets.Exceptions;
using Shearlegs.Web.API.Models.PluginSecrets.Params;
using Shearlegs.Web.API.Models.PluginSecrets.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.PluginSecrets
{
    public class PluginSecretService : IPluginSecretService
    {
        private readonly IStorageBroker storageBroker;

        public PluginSecretService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<IEnumerable<PluginSecret>> RetrievePluginSecretsByPluginIdAsync(int pluginId)
        {
            GetPluginSecretsParams @params = new()
            {
                PluginId = pluginId
            };

            IEnumerable<PluginSecret> pluginSecrets = await storageBroker.GetPluginSecretsAsync(@params);

            return pluginSecrets;
        }

        public async ValueTask<PluginSecret> RetrievePluginSecretByIdAsync(int pluginSecretId)
        {
            GetPluginSecretsParams @params = new()
            {
                PluginSecretId = pluginSecretId
            };

            PluginSecret pluginSecret = await storageBroker.GetPluginSecretAsync(@params);

            if (pluginSecret == null)
            {
                throw new NotFoundPluginSecretException();
            }

            return pluginSecret;
        }

        public async ValueTask<PluginSecret> UpdatePluginSecretAsync(UpdatePluginSecretParams @params)
        {
            StoredProcedureResult result = await storageBroker.UpdatePluginSecretAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundPluginSecretException();
            }

            PluginSecret pluginSecret = await RetrievePluginSecretByIdAsync(@params.PluginSecretId);

            return pluginSecret;
        }

        public async ValueTask<PluginSecret> AddPluginSecretAsync(AddPluginSecretParams @params)
        {
            AddPluginSecretResult result = await storageBroker.AddPluginSecretAsync(@params);

            if (result.StoredProcedureResult.ReturnValue == 1)
            {
                throw new AlreadyExistsPluginSecretException();
            }

            if (result.StoredProcedureResult.ReturnValue == 2)
            {
                throw new NotFoundPluginException();
            }

            PluginSecret pluginSecret = await RetrievePluginSecretByIdAsync(result.PluginSecretId.Value);

            return pluginSecret;
        }
    }
}
