using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.PluginSecrets;
using Shearlegs.Web.API.Models.Results;
using Shearlegs.Web.API.Models.Results.Params;
using Shearlegs.Web.API.Models.ShearlegsFrameworks;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Params;
using Shearlegs.Web.API.Models.ShearlegsFrameworks.Results;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Params;
using Shearlegs.Web.API.Services.Foundations.Plugins;
using Shearlegs.Web.API.Services.Foundations.PluginSecrets;
using Shearlegs.Web.API.Services.Foundations.Results;
using Shearlegs.Web.API.Services.Foundations.Schedulings;
using Shearlegs.Web.API.Services.Foundations.ShearlegsFrameworks;
using Shearlegs.Web.API.Services.Processings.Results;
using Shearlegs.Web.API.Services.Processings.Versions;
using Shearlegs.Web.Shared.Enums;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Versions
{
    public partial class VersionOrchestrationService : IVersionOrchestrationService
    {
        private readonly IVersionProcessingService versionService;
        private readonly IPluginService pluginService;
        private readonly IShearlegsFrameworkService shearlegsFrameworkService;
        private readonly IPluginSecretService pluginSecretService;
        private readonly IResultProcessingService resultService;
        private readonly ISchedulingService schedulingService;

        public VersionOrchestrationService(
            IVersionProcessingService versionService,
            IPluginService pluginService,
            IShearlegsFrameworkService shearlegsFrameworkService,
            IPluginSecretService pluginSecretService,
            IResultProcessingService resultService,
            ISchedulingService schedulingService)
        {
            this.versionService = versionService;
            this.pluginService = pluginService;
            this.shearlegsFrameworkService = shearlegsFrameworkService;
            this.pluginSecretService = pluginSecretService;
            this.resultService = resultService;
            this.schedulingService = schedulingService;
        }

        public async ValueTask<Version> UploadVersionAsync(IFormFile formFile)
        {         
            byte[] fileData;
            using (MemoryStream ms = new())
            {
                await formFile.CopyToAsync(ms);
                fileData = ms.ToArray();
            }

            GetShearlegsPluginInfoParams getShearlegsPluginInfoParams = new()
            {
                PluginData =  fileData
            };

            ShearlegsPluginInfo shearlegsPluginInfo = await shearlegsFrameworkService.GetShearlegsPluginInfoAsync(getShearlegsPluginInfoParams);
            
            Plugin plugin = await pluginService.RetrievePluginByPackageId(shearlegsPluginInfo.PackageId);

            CreateVersionParams createVersionParams = new()
            {
                Content = fileData,
                Name = shearlegsPluginInfo.Version,
                PluginId = plugin.Id,
                CreateUserId = null,
                Parameters = new()
            };

            foreach (ShearlegsPluginParameterInfo shearlegsPluginParameterInfo in shearlegsPluginInfo.Parameters)
            {
                createVersionParams.Parameters.Add(new CreateVersionParams.VersionParameter()
                {
                    Name = shearlegsPluginParameterInfo.Name,
                    DataType = shearlegsPluginParameterInfo.Type,
                    DefaultValue = shearlegsPluginParameterInfo.Value,
                    Description = shearlegsPluginParameterInfo.Description,
                    IsSecret = shearlegsPluginParameterInfo.IsSecret,
                    IsArray = shearlegsPluginParameterInfo.IsArray,
                    IsRequired = shearlegsPluginParameterInfo.IsRequired
                });
            }

            Version version = await versionService.CreateVersionAsync(createVersionParams);

            return version;            
        }

        public async ValueTask QueueExecuteVersionAsync(ExecuteVersionParams @params)
        {
            ValidateExecuteVersionParams(@params);

            Version version = await versionService.RetrieveVersionByIdAsync(@params.VersionId);
            Plugin plugin = await pluginService.RetrievePluginByVersionIdAsync(@params.VersionId);

            byte[] parametersData = Encoding.UTF8.GetBytes(@params.ParametersJson);

            AddResultParams addResultParams = new()
            {
                VersionId = version.Id,
                Status = ResultStatus.Queued,
                ParametersData = parametersData,
                UserId = @params.UserId
            };

            Result result = await resultService.AddResultAsync(addResultParams);
            string jobId = await schedulingService.EnqueueAsync(() => ExecuteResultAsync(result.Id));            
        }

        public async Task ExecuteResultAsync(int resultId)
        {
            Result result = await resultService.RetrieveResultByIdAsync(resultId);

            VersionContent versionContent = await versionService.RetrieveVersionContentByIdAsync(result.Version.Id);
            IEnumerable<PluginSecret> pluginSecrets = await pluginSecretService.RetrievePluginSecretsByPluginIdAsync(result.Plugin.Id);

            ResultParameters resultParameters = await resultService.RetrieveResultParametersByIdAsync(resultId);
            string parametersWithSecrets = AppendPluginSecretsToParameters(resultParameters.ParametersJson, pluginSecrets);

            UpdateResultStatusParams updateResultStatusParams = new()
            {
                ResultId = result.Id,
                Status = ResultStatus.InProgress
            };

            result = await resultService.UpdateResultStatusAsync(updateResultStatusParams);

            ExecuteShearlegsPluginParams executeShearlegsPluginParams = new()
            {
                PluginData = versionContent.Content,
                ParametersJson = parametersWithSecrets
            };

            ShearlegsPluginResult shearlegsResult = await shearlegsFrameworkService.ExecuteShearlegsPluginAsync(executeShearlegsPluginParams);

            string shearlegsResultJson = JsonConvert.SerializeObject(shearlegsResult);
            byte[] shearlegsResultData = Encoding.UTF8.GetBytes(shearlegsResultJson);
            ResultStatus resultStatus = shearlegsResult.ResultType == "Error" ? ResultStatus.Failed : ResultStatus.Completed;

            UpdateResultParams updateResultParams = new()
            {
                ResultId = result.Id,
                Status = resultStatus,
                ResultData = shearlegsResultData,
                ResultType = shearlegsResult.ResultType
            };

            result = await resultService.UpdateResultAsync(updateResultParams);
        }
    }
}
