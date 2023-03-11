using Microsoft.AspNetCore.Http;
using Shearlegs.Web.API.Brokers.Serializations;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Params;
using Shearlegs.Web.API.Services.Foundations.Versions;
using System.IO;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Processings.Versions
{
    public class VersionProcessingService : IVersionProcessingService
    {
        private readonly IVersionService versionService;
        private readonly ISerializationBroker serializationBroker;

        public VersionProcessingService(IVersionService versionService, ISerializationBroker serializationBroker)
        {
            this.versionService = versionService;
            this.serializationBroker = serializationBroker;
        }

        public async ValueTask<Version> RetrieveVersionByIdAsync(int versionId)
        {
            return await versionService.RetrieveVersionByIdAsync(versionId);
        }

        public async ValueTask<VersionContent> RetrieveVersionContentByIdAsync(int versionId)
        {
            return await versionService.RetrieveVersionContentByIdAsync(versionId);
        }

        public async ValueTask<Version> CreateVersionAsync(CreateVersionParams @params)
        {
            AddVersionParams addVersionParams = new()
            {
                PluginId = @params.PluginId,
                Name = @params.Name,
                Content = @params.Content,
                ContentLength = @params.Content.Length,
                CreateUserId = @params.CreateUserId,
                ParametersJson = serializationBroker.SerializeToJson(@params.Parameters)
            };

            return await versionService.AddVersionAsync(addVersionParams);
        }
    }
}
