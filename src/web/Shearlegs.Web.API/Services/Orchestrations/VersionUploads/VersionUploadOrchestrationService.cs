using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Models.VersionUploads.Params;
using Shearlegs.Web.API.Services.Foundations.VersionUploads;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.VersionUploads
{
    public class VersionUploadOrchestrationService : IVersionUploadOrchestrationService
    {
        private readonly IVersionUploadService versionUploadService;

        public VersionUploadOrchestrationService(IVersionUploadService versionUploadService)
        {
            this.versionUploadService = versionUploadService;
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

        public async ValueTask<VersionUpload> AddVersionUploadAsync(AddVersionUploadParams @params)
        {
            VersionUpload versionUpload = await versionUploadService.AddVersionUploadAsync(@params);

            return versionUpload;
        }
    }
}
