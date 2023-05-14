using Microsoft.AspNetCore.Http;
using Shearlegs.Web.API.Models.UserAuthentications;
using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Models.VersionUploads.Params;
using Shearlegs.Web.API.Services.Orchestrations.UserAuthentications;
using Shearlegs.Web.API.Services.Orchestrations.VersionUploads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Coordinations.VersionUploadUserAuthentications
{
    public class VersionUploadUserAuthenticationCoordinationService : IVersionUploadUserAuthenticationCoordinationService
    {
        private readonly IVersionUploadOrchestrationService versionUploadService;
        private readonly IUserAuthenticationOrchestrationService userAuthenticationService;

        public VersionUploadUserAuthenticationCoordinationService(
            IVersionUploadOrchestrationService versionUploadService, 
            IUserAuthenticationOrchestrationService userAuthenticationService)
        {
            this.versionUploadService = versionUploadService;
            this.userAuthenticationService = userAuthenticationService;
        }

        public async ValueTask<VersionUpload> AddUserVersionUploadAsync(IFormFile formFile)
        {
            byte[] fileData;
            using (MemoryStream ms = new())
            {
                await formFile.CopyToAsync(ms);
                fileData = ms.ToArray();
            }

            AuthenticatedUser authenticatedUser = await userAuthenticationService.RetrieveAuthenticatedUserAsync();

            AddVersionUploadParams @params = new()
            {
                UserId = authenticatedUser.User.Id,
                FileName = formFile.FileName,
                ContentType = formFile.ContentType,
                Content = fileData,
                ContentLength = fileData.LongLength
            };

            if (DateTimeOffset.TryParse(formFile.Headers.LastModified, out DateTimeOffset lastModified))
            {
                @params.LastModified = lastModified;
            }

            return await versionUploadService.AddVersionUploadAsync(@params);
        }

        public async ValueTask<VersionUpload> RetrieveVersionUploadByIdAsync(int versionUploadId)
        {
            return await versionUploadService.RetrieveVersionUploadByIdAsync(versionUploadId);
        }

        public async ValueTask<IEnumerable<VersionUpload>> RetrieveVersionUploadsByUserIdAsync(int userId)
        {
            return await versionUploadService.RetrieveVersionUploadsByUserIdAsync(userId);
        }

        public async ValueTask<IEnumerable<VersionUpload>> RetrieveAllVersionUploadsAsync()
        {
            return await versionUploadService.RetrieveAllVersionUploadsAsync();
        }
    }
}
