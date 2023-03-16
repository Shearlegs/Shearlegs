using Newtonsoft.Json.Linq;
using Shearlegs.Web.API.Models.PluginSecrets;
using Shearlegs.Web.API.Models.Results;
using System.Collections;
using System.Collections.Generic;

namespace Shearlegs.Web.API.Services.Orchestrations.Versions
{
    public partial class VersionOrchestrationService
    {
        private string AppendPluginSecretsToParameters(string parametersJson, IEnumerable<PluginSecret> pluginSecrets)
        {
            JObject jObject = JObject.Parse(parametersJson);

            foreach (PluginSecret pluginSecret in pluginSecrets)
            {
                jObject.Add(pluginSecret.Name, JToken.FromObject(pluginSecret.Value));
            }

            return jObject.ToString();
        }
    }
}
