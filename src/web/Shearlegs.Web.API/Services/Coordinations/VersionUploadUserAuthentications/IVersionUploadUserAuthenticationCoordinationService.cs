using Microsoft.AspNetCore.Http;
using Shearlegs.Web.API.Models.VersionUploads;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Coordinations.VersionUploadUserAuthentications
{
    public interface IVersionUploadUserAuthenticationCoordinationService
    {
        ValueTask<VersionUpload> AddUserVersionUploadAsync(IFormFile formFile);
        ValueTask<IEnumerable<VersionUpload>> RetrieveAllVersionUploadsAsync();
        ValueTask<VersionUpload> RetrieveVersionUploadByIdAsync(int versionUploadId);
        ValueTask<IEnumerable<VersionUpload>> RetrieveVersionUploadByUserIdAsync(int userId);
    }
}