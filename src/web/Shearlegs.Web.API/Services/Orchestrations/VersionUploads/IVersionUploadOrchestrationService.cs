using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Models.VersionUploads.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.VersionUploads
{
    public interface IVersionUploadOrchestrationService
    {
        ValueTask<VersionUpload> AddVersionUploadAsync(AddVersionUploadParams @params);
        ValueTask<IEnumerable<VersionUpload>> RetrieveAllVersionUploadsAsync();
        ValueTask<VersionUpload> RetrieveVersionUploadByIdAsync(int versionUploadId);
        ValueTask<IEnumerable<VersionUpload>> RetrieveVersionUploadsByUserIdAsync(int userId);
    }
}