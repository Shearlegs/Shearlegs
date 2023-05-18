using Microsoft.AspNetCore.Http;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.VersionUploads;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Managements.VersionUploadUserAuthentications
{
    public interface IVersionUploadUserAuthenticationManagementService
    {
        ValueTask<VersionUpload> AddUserVersionUploadAsync(IFormFile formFile);
        ValueTask<Version> MigrateVersionUploadToVersionAsync(int versionUploadId);
        ValueTask<IEnumerable<VersionUpload>> RetrieveAllVersionUploadsAsync();
        ValueTask<VersionUpload> RetrieveVersionUploadByIdAsync(int versionUploadId);
        ValueTask<IEnumerable<VersionUpload>> RetrieveVersionUploadsByUserIdAsync(int userId);
    }
}