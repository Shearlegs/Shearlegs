using Newtonsoft.Json.Linq;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Services
{
    public class PluginService
    {
        private readonly IPluginManager pluginManager;
        private readonly VersionsRepository versionsRepository;
        private readonly ResultsRepository resultsRepository;

        public PluginService(IPluginManager pluginManager, VersionsRepository versionsRepository, ResultsRepository resultsRepository)
        {
            this.pluginManager = pluginManager;
            this.versionsRepository = versionsRepository;
            this.resultsRepository = resultsRepository;
        }

        public async Task<int> ExecuteVersionAsync(int userId, string username, int versionId, string parametersJson)
        {
            JObject jObject = JObject.Parse(parametersJson);
            IEnumerable<MPluginSecret> secrets = await versionsRepository.GetVersionSecretsAsync(versionId);
            foreach (MPluginSecret secret in secrets)
            {
                jObject.Add(secret.Name, JToken.FromObject(secret.Value));
            }

            if (!jObject.ContainsKey("Username"))
            {
                jObject.Add("Username", username);
            }

            byte[] content = await versionsRepository.GetVersionPackageContent(versionId);
            IPluginResult pluginResult = await pluginManager.ExecutePluginAsync(content, jObject.ToString());
            MResult result = MResult.Create(pluginResult, parametersJson, versionId, userId);
            return await resultsRepository.AddResultAsync(result);
        }
    }
}
