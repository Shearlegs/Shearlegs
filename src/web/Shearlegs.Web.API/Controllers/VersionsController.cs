using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.Plugins.Exceptions;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Exceptions;
using Shearlegs.Web.API.Models.Versions.Params;
using Shearlegs.Web.API.Services.Foundations.Versions;
using Shearlegs.Web.API.Services.Processings.Versions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VersionsController : RESTFulController
    {
        private readonly IVersionService versionService;
        private readonly IVersionProcessingService versionProcessingService;

        public VersionsController(IVersionService versionService, IVersionProcessingService versionProcessingService)
        {
            this.versionService = versionService;
            this.versionProcessingService = versionProcessingService;
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

        [HttpPost("create")]
        public async ValueTask<IActionResult> CreateVersion([FromBody] CreateVersionParams @params)
        {
            try
            {
                Version version = await versionProcessingService.CreateVersionAsync(@params);

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
