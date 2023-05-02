using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.API.Constants;
using Shearlegs.API.Plugins;
using Shearlegs.Runtime;
using Shearlegs.Web.Node.Helpers;
using Shearlegs.Web.Node.Models;

namespace Shearlegs.Web.Node.Controllers
{
    [ApiController]
    public class MainController : RESTFulController
    {
        private readonly IPluginManager pluginManager;

        public MainController(IPluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            NodeInfo nodeInfo = new()
            {
                NodeVersion = typeof(Program).Assembly.GetName().Version.ToString(3),
                ShearlegsRuntimeVersion = typeof(ShearlegsRuntime).Assembly.GetName().Version.ToString(3)
            };

            return Ok(nodeInfo);
        }        

        [HttpGet("fullinfo")]
        public IActionResult GetFullInfo()
        {
            NodeFullInfo nodeFullInfo = new()
            {
                NodeVersion = typeof(Program).Assembly.GetName().Version.ToString(3),
                ShearlegsRuntimeVersion = typeof(ShearlegsRuntime).Assembly.GetName().Version.ToString(3),
                CacheSizeBytes = DirectoryHelper.GetDirectorySize(DirectoryConstants.CacheDirectory)
            };

            return Ok(nodeFullInfo);
        }
    }
}
