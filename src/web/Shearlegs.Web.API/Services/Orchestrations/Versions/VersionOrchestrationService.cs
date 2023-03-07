using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Params;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Results;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Params;
using Shearlegs.Web.API.Services.Foundations.Plugins;
using Shearlegs.Web.API.Services.Foundations.ShearlegsFrameworks;
using Shearlegs.Web.API.Services.Foundations.Versions;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Versions
{
    public class VersionOrchestrationService : IVersionOrchestrationService
    {
        private readonly IVersionService versionService;
        private readonly IPluginService pluginService;
        private readonly IShearlegsFrameworkService shearlegsFrameworkService;

        public VersionOrchestrationService(
            IVersionService versionService, 
            IPluginService pluginService, 
            ShearlegsFrameworkService shearlegsFrameworkService)
        {
            this.versionService = versionService;
            this.pluginService = pluginService;
            this.shearlegsFrameworkService = shearlegsFrameworkService;
        }

        public async ValueTask ExecuteVersionAsync(ExecuteVersionParams @params)
        {
            Version version = await versionService.RetrieveVersionByIdAsync(@params.VersionId);
            Plugin plugin = await pluginService.RetrievePluginByVersionIdAsync(@params.VersionId);

            VersionContent versionContent = await versionService.RetrieveVersionContentByIdAsync(@params.VersionId);

            ExecuteShearlegsPluginParams executeShearlegsPluginParams = new()
            {
                PluginData = versionContent.Content,
                ParametersJson = @params.ParametersJson
            };

            ShearlegsPluginResult result = await shearlegsFrameworkService.ExecuteShearlegsPluginAsync(executeShearlegsPluginParams);
        }
    }
}
