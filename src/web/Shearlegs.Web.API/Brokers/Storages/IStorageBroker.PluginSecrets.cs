using Shearlegs.Web.API.Models.PluginSecrets.Params;
using Shearlegs.Web.API.Models.PluginSecrets;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using Shearlegs.Web.API.Models.PluginSecrets.Results;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<AddPluginSecretResult> AddPluginSecretAsync(AddPluginSecretParams @params);
        ValueTask<PluginSecret> GetPluginSecretAsync(GetPluginSecretsParams @params);
        ValueTask<IEnumerable<PluginSecret>> GetPluginSecretsAsync(GetPluginSecretsParams @params);
        ValueTask<StoredProcedureResult> UpdatePluginSecretAsync(UpdatePluginSecretParams @params);
    }
}