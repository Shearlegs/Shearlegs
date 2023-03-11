using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json.Linq;
using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.PluginSecrets;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Params;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Results;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Params;
using Shearlegs.Web.API.Services.Foundations.Plugins;
using Shearlegs.Web.API.Services.Foundations.PluginSecrets;
using Shearlegs.Web.API.Services.Foundations.ShearlegsFrameworks;
using Shearlegs.Web.API.Services.Foundations.Versions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Versions
{
    public class VersionOrchestrationService : IVersionOrchestrationService
    {
        private readonly IVersionService versionService;
        private readonly IPluginService pluginService;
        private readonly IShearlegsFrameworkService shearlegsFrameworkService;
        private readonly IPluginSecretService pluginSecretService;

        public VersionOrchestrationService(
            IVersionService versionService,
            IPluginService pluginService,
            IShearlegsFrameworkService shearlegsFrameworkService,
            IPluginSecretService pluginSecretService)
        {
            this.versionService = versionService;
            this.pluginService = pluginService;
            this.shearlegsFrameworkService = shearlegsFrameworkService;
            this.pluginSecretService = pluginSecretService;
        }

        public async ValueTask ExecuteVersionAsync(ExecuteVersionParams @params)
        {
            Version version = await versionService.RetrieveVersionByIdAsync(@params.VersionId);
            Plugin plugin = await pluginService.RetrievePluginByVersionIdAsync(@params.VersionId);

            VersionContent versionContent = await versionService.RetrieveVersionContentByIdAsync(version.Id);
            IEnumerable<PluginSecret> pluginSecrets = await pluginSecretService.RetrievePluginSecretsByPluginIdAsync(plugin.Id);

            JObject jObject = JObject.Parse(@params.ParametersJson);

            foreach (PluginSecret pluginSecret in pluginSecrets)
            {
                jObject.Add(pluginSecret.Name, JToken.FromObject(pluginSecret.Value));
            }

            ExecuteShearlegsPluginParams executeShearlegsPluginParams = new()
            {
                PluginData = versionContent.Content,
                ParametersJson = jObject.ToString()
            };

            ShearlegsPluginResult result = await shearlegsFrameworkService.ExecuteShearlegsPluginAsync(executeShearlegsPluginParams);


        }
    }
}
