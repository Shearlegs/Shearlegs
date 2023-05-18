using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Models.VersionUploads.Params;
using Shearlegs.Web.API.Services.Foundations.Versions;
using Shearlegs.Web.API.Services.Foundations.VersionUploads;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.VersionUploads
{
    public class VersionUploadOrchestrationService : IVersionUploadOrchestrationService
    {
        private readonly IVersionUploadService versionUploadService;
        private readonly IVersionService versionService;

        public VersionUploadOrchestrationService(IVersionUploadService versionUploadService, IVersionService versionService)
        {
            this.versionUploadService = versionUploadService;
            this.versionService = versionService;
        }

        public async ValueTask<VersionUpload> RetrieveVersionUploadByIdAsync(int versionUploadId)
        {
            VersionUpload versionUpload = await versionUploadService.RetrieveVersionUploadByIdAsync(versionUploadId);

            return versionUpload;
        }

        public async ValueTask<IEnumerable<VersionUpload>> RetrieveAllVersionUploadsAsync()
        {
            IEnumerable<VersionUpload> versionUploads = await versionUploadService.RetrieveAllVersionUploadsAsync();

            return versionUploads;
        }

        public async ValueTask<IEnumerable<VersionUpload>> RetrieveVersionUploadsByUserIdAsync(int userId)
        {
            IEnumerable<VersionUpload> versionUploads = await versionUploadService.RetrieveVersionUploadByUserIdAsync(userId);

            return versionUploads;
        }

        public async ValueTask<VersionUploadContent> RetrieveVersionUploadContentByIdAsync(int versionUploadId)
        {
            VersionUploadContent versionUploadContent = await versionUploadService.RetrieveVersionUploadContentByIdAsync(versionUploadId);

            return versionUploadContent;
        }

        public async ValueTask<VersionUpload> AddVersionUploadAsync(AddVersionUploadParams @params)
        {
            VersionUpload versionUpload = await versionUploadService.AddVersionUploadAsync(@params);

            return versionUpload;
        }

        public async ValueTask<VersionUpload> StartProcessingVersionUploadAsync(StartProcessingVersionUploadParams @params)
        {
            VersionUpload versionUpload = await versionUploadService.StartProcessingVersionUploadAsync(@params);

            return versionUpload;
        }

        public async ValueTask<VersionUpload> FinishProcessingVersionUploadAsync(FinishProcessingVersionUploadParams @params)
        {
            VersionUpload versionUpload = await versionUploadService.FinishProcessingVersionUploadAsync(@params);

            return versionUpload;
        }

        public async ValueTask<Version> MigrateVersionUploadToVersionAsync(int versionUploadId)
        {
            Version version = await versionService.MigrateVersionUploadToVersionAsync(versionUploadId);

            return version;
        }
    }
}
