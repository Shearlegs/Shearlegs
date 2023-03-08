using Shearlegs.Web.API.Models.PluginSecrets;
using Shearlegs.Web.API.Models.PluginSecrets.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.PluginSecrets
{
    public interface IPluginSecretService
    {
        ValueTask<PluginSecret> AddPluginSecretAsync(AddPluginSecretParams @params);
        ValueTask<PluginSecret> RetrievePluginSecretByIdAsync(int pluginSecretId);
        ValueTask<IEnumerable<PluginSecret>> RetrievePluginSecretsByPluginIdAsync(int pluginId);
        ValueTask<PluginSecret> UpdatePluginSecretAsync(UpdatePluginSecretParams @params);
    }
}