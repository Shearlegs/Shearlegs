using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Models.VersionUploads.Exceptions;
using Shearlegs.Web.API.Services.Coordinations.VersionUploadUserAuthentications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("versionuploads")]
    public class VersionUploadsController : RESTFulController
    {
        private readonly IVersionUploadUserAuthenticationCoordinationService versionUploadUserAuthenticationService;

        public VersionUploadsController(IVersionUploadUserAuthenticationCoordinationService versionUploadUserAuthenticationService)
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
            IEnumerable<VersionUpload> versionUploads = await versionUploadUserAuthenticationService.RetrieveVersionUploadByUserIdAsync(userId);

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
    }
}
