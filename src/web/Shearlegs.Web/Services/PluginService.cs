using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Web.Database.Repositories;
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

        public PluginService(IPluginManager pluginManager, VersionsRepository versionsRepository)
        {
            this.pluginManager = pluginManager;
            this.versionsRepository = versionsRepository;
        }

        public async Task<IPluginResult> ExecuteVersionAsync(int versionId, string parametersJson)
        {
            byte[] content = await versionsRepository.GetVersionPackageContent(versionId);
            return await pluginManager.ExecutePluginAsync(content, parametersJson);
        }


    }
}
