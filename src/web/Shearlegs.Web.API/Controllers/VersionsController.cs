using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.Plugins.Exceptions;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Exceptions;
using Shearlegs.Web.API.Models.Versions.Params;
using Shearlegs.Web.API.Services.Foundations.Versions;
using Shearlegs.Web.API.Services.Orchestrations.Versions;
using Shearlegs.Web.API.Services.Processings.Versions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("versions")]
    public class VersionsController : RESTFulController
    {
        private readonly IVersionService versionService;
        private readonly IVersionProcessingService versionProcessingService;
        private readonly IVersionOrchestrationService versionOrchestrationService;

        public VersionsController(
            IVersionService versionService, 
            IVersionProcessingService versionProcessingService, 
            IVersionOrchestrationService versionOrchestrationService)
        {
            this.versionService = versionService;
            this.versionProcessingService = versionProcessingService;
            this.versionOrchestrationService = versionOrchestrationService;
        }

        [HttpGet("{versionId}")]
        public async ValueTask<IActionResult> GetVersionById(int versionId)
        {
            try
            {
                Version version = await versionService.RetrieveVersionByIdAsync(versionId);

                return Ok(version);
            } catch (NotFoundVersionException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllVersions()
        {
            IEnumerable<Version> versions = await versionService.RetrieveAllVersionsAsync();

            return Ok(versions);
        }

        [HttpPost("execute")]
        public async ValueTask<IActionResult> ExecuteAsync([FromBody] ExecuteVersionParams @params)
        {
            try
            {
                await versionOrchestrationService.QueueExecuteVersionAsync(@params);

                return Ok();
            } catch (ExecuteVersionParamsValidationException exception)
            {
                return BadRequest(exception);
            }            
        }

        [HttpPost("upload")]
        public async ValueTask<IActionResult> UploadVersion(IFormFile formFile)
        {
            try
            {
                Version version = await versionOrchestrationService.UploadVersionAsync(formFile);

                return Ok(version);
            } catch (NotFoundPluginException exception)
            {
                return NotFound(exception);
            } catch (AlreadyExistsVersionException exception)
            {
                return Conflict(exception);
            }
        }
    }
}
