using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.Plugins.Exceptions;
using Shearlegs.Web.API.Models.PluginSecrets;
using Shearlegs.Web.API.Models.PluginSecrets.Exceptions;
using Shearlegs.Web.API.Models.PluginSecrets.Params;
using Shearlegs.Web.API.Services.Foundations.PluginSecrets;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("plugins/secrets")]
    public class PluginSecretsController : RESTFulController
    {
        private readonly IPluginSecretService pluginSecretService;

        public PluginSecretsController(IPluginSecretService pluginSecretService)
        {
            this.pluginSecretService = pluginSecretService;
        }

        [HttpGet("{pluginSecretId}")]
        public async ValueTask<IActionResult> GetPluginSecretById(int pluginSecretId)
        {
            try
            {
                PluginSecret pluginSecret = await pluginSecretService.RetrievePluginSecretByIdAsync(pluginSecretId);

                return Ok(pluginSecret);
            } catch (NotFoundPluginSecretException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpPost("add")]
        public async ValueTask<IActionResult> AddPluginSecret([FromBody] AddPluginSecretParams @params)
        {
            try
            {
                PluginSecret pluginSecret = await pluginSecretService.AddPluginSecretAsync(@params);

                return Ok(pluginSecret);
            } catch (AlreadyExistsPluginSecretException exception)
            {
                return Conflict(exception);
            } catch (NotFoundPluginException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpPost("update")]
        public async ValueTask<IActionResult> UpdatePluginSecret([FromBody] UpdatePluginSecretParams @params)
        {
            try
            {
                PluginSecret pluginSecret = await pluginSecretService.UpdatePluginSecretAsync(@params);

                return Ok(pluginSecret);
            } catch (NotFoundPluginSecretException exception)
            {
                return NotFound(exception);
            }
        }
    }
}
