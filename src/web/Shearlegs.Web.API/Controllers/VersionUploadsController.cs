using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Exceptions;
using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Models.VersionUploads.Exceptions;
using Shearlegs.Web.API.Services.Managements.VersionUploadUserAuthentications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("versionuploads")]
    public class VersionUploadsController : RESTFulController
    {
        private readonly IVersionUploadUserAuthenticationManagementService versionUploadUserAuthenticationService;

        public VersionUploadsController(IVersionUploadUserAuthenticationManagementService versionUploadUserAuthenticationService)
        {
            this.versionUploadUserAuthenticationService = versionUploadUserAuthenticationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetVersionUploads()
        {
            IEnumerable<VersionUpload> versionUploads = await versionUploadUserAuthenticationService.RetrieveAllVersionUploadsAsync();

            return Ok(versionUploads);
        }

        [HttpGet("{versionUploadId}")]
        public async ValueTask<IActionResult> GetVersionUploadById(int versionUploadId)
        {
            try
            {
                VersionUpload versionUpload = await versionUploadUserAuthenticationService.RetrieveVersionUploadByIdAsync(versionUploadId);

                return Ok(versionUpload);
            } catch (NotFoundVersionUploadException exception)
            {
                return NotFound(exception);
            }            
        }

        [HttpGet("userId/{userId}")]
        public async ValueTask<IActionResult> GetVersionUploadByUserId(int userId)
        {
            IEnumerable<VersionUpload> versionUploads = await versionUploadUserAuthenticationService.RetrieveVersionUploadsByUserIdAsync(userId);

            return Ok(versionUploads);
        }

        [HttpPost("add")]
        public async ValueTask<IActionResult> AddVersionUploadAsync(IFormFile formFile)
        {
            try
            {
                VersionUpload versionUpload = await versionUploadUserAuthenticationService.AddUserVersionUploadAsync(formFile);

                return Ok(versionUpload);
            } catch (NotFoundVersionUploadException exception)
            {
                return NotFound(exception);
            }            
        }

        [HttpPost("{versionUploadId}/migrate")]
        public async ValueTask<IActionResult> MigrateVersionUploadToVersionAsync(int versionUploadId)
        {
            try
            {
                Version version = await versionUploadUserAuthenticationService.MigrateVersionUploadToVersionAsync(versionUploadId);

                return Ok(version);
            } catch (NotFoundVersionUploadException exception)
            {
                return NotFound(exception);
            } catch (NotFoundVersionException exception)
            {
                return NotFound(exception);
            } catch (AlreadyExistsVersionException exception)
            {
                return Conflict(exception);
            }
        }
    }
}
