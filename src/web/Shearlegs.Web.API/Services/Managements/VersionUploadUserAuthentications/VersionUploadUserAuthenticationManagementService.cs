using Microsoft.AspNetCore.Http;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Services.Coordinations.VersionUploads;
using Shearlegs.Web.API.Services.Coordinations.VersionUploadUserAuthentications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Managements.VersionUploadUserAuthentications
{
    public class VersionUploadUserAuthenticationManagementService : IVersionUploadUserAuthenticationManagementService
    {
        private readonly IVersionUploadUserAuthenticationCoordinationService versionUploadUserAuthenticationService;
        private readonly IVersionUploadCoordinationService versionUploadService;

        public VersionUploadUserAuthenticationManagementService(
            IVersionUploadUserAuthenticationCoordinationService versionUploadUserAuthenticationService, 
            IVersionUploadCoordinationService versionUploadService)
        {
            this.versionUploadUserAuthenticationService = versionUploadUserAuthenticationService;
            this.versionUploadService = versionUploadService;
        }

        public async ValueTask<VersionUpload> AddUserVersionUploadAsync(IFormFile formFile)
        {
            VersionUpload versionUpload = await versionUploadUserAuthenticationService.AddUserVersionUploadAsync(formFile);
            await versionUploadService.QueueProcessVersionAsync(versionUpload.Id);

            return versionUpload;
        }

        public async ValueTask<VersionUpload> RetrieveVersionUploadByIdAsync(int versionUploadId)
        {
            return await versionUploadUserAuthenticationService.RetrieveVersionUploadByIdAsync(versionUploadId);
        }

        public async ValueTask<IEnumerable<VersionUpload>> RetrieveAllVersionUploadsAsync()
        {
            return await versionUploadUserAuthenticationService.RetrieveAllVersionUploadsAsync();
        }

        public async ValueTask<IEnumerable<VersionUpload>> RetrieveVersionUploadsByUserIdAsync(int userId)
        {
            return await versionUploadUserAuthenticationService.RetrieveVersionUploadsByUserIdAsync(userId);
        }

        public async ValueTask<Version> MigrateVersionUploadToVersionAsync(int versionUploadId)
        {
            return await versionUploadUserAuthenticationService.MigrateVersionUploadToVersionAsync(versionUploadId);
        }
    }
}
