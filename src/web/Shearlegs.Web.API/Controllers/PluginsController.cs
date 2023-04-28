using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.Plugins.Exceptions;
using Shearlegs.Web.API.Models.Plugins.Params;
using Shearlegs.Web.API.Models.PluginSecrets;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Services.Foundations.Plugins;
using Shearlegs.Web.API.Services.Foundations.PluginSecrets;
using Shearlegs.Web.API.Services.Foundations.Versions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("plugins")]
    public class PluginsController : RESTFulController
    {
        private readonly IPluginService pluginService;
        private readonly IVersionService versionService;
        private readonly IPluginSecretService pluginSecretService;

        public PluginsController(IPluginService pluginService, IVersionService versionService, IPluginSecretService pluginSecretService)
        {
            this.pluginService = pluginService;
            this.versionService = versionService;
            this.pluginSecretService = pluginSecretService;
        }

        [HttpGet("{pluginId}")]
        public async ValueTask<IActionResult> GetPluginById(int pluginId)
        {
            try
            {
                Plugin plugin = await pluginService.RetrievePluginByIdAsync(pluginId);

                return Ok(plugin);
            } catch (NotFoundPluginException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpGet("packageId/{packageId}")]
        public async ValueTask<IActionResult> GetPluginByPackageId(string packageId)
        {
            try
            {
                Plugin plugin = await pluginService.RetrievePluginByPackageId(packageId);

                return Ok(plugin);
            }
            catch (NotFoundPluginException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllPlugins()
        {
            IEnumerable<Plugin> plugins = await pluginService.RetrieveAllPluginsAsync();

            return Ok(plugins);
        }

        [HttpGet("{pluginId}/versions")]
        public async ValueTask<IActionResult> GetVersionsByPluginId(int pluginId)
        {
            IEnumerable<Version> versions = await versionService.RetrieveVersionsByPluginIdAsync(pluginId);

            return Ok(versions);
        }

        [HttpGet("{pluginId}/secrets")]
        public async ValueTask<IActionResult> GetPluginSecretsByPluginId(int pluginId)
        {
            IEnumerable<PluginSecret> pluginSecrets = await pluginSecretService.RetrievePluginSecretsByPluginIdAsync(pluginId);

            return Ok(pluginSecrets);
        }

        [HttpPost("add")]
        public async ValueTask<IActionResult> AddPlugin([FromBody] AddPluginParams @params)
        {
            try
            {
                Plugin plugin = await pluginService.AddPluginAsync(@params);

                return Ok(plugin);
            } catch (AlreadyExistsPluginException exception)
            {
                return Conflict(exception);
            }
        }

        [HttpPost("{pluginId}/update")]
        public async ValueTask<IActionResult> UpdatePlugin(int pluginId, [FromBody] UpdatePluginParams @params)
        {
            try
            {
                @params.PluginId = pluginId;
                Plugin plugin = await pluginService.UpdatePluginAsync(@params);

                return Ok(plugin);
            }
            catch (NotFoundPluginException exception)
            {
                return NotFound(exception);
            } catch (AlreadyExistsPluginException exception)
            {
                return Conflict(exception);
            }
        }
    }
}
