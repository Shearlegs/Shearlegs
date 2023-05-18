using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RESTFulSense.Controllers;
using Shearlegs.API.Constants;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins.Info;
using Shearlegs.Runtime;
using Shearlegs.Web.Node.Helpers;
using Shearlegs.Web.Node.Models;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet("statistics")]
        public IActionResult GetStatistics()
        {
            NodeStatistics nodeStatistics = new()
            {
                CacheSizeBytes = DirectoryHelper.GetDirectorySize(DirectoryConstants.CacheDirectory)
            };

            return Ok(nodeStatistics);
        }

        [HttpPost("process-plugin")]
        public async ValueTask<IActionResult> TestPlugin(IFormFile formFile)
        {
            using Stream stream = formFile.OpenReadStream();
            IPluginInfo pluginInfo = await pluginManager.GetPluginInfoAsync(stream);

            PluginInformation pluginInformation = new()
            {
                PackageId = pluginInfo.PackageId,
                Version = pluginInfo.Version,
                IsPrerelease = pluginInfo.IsPrerelease,
                Parameters = new(),
                ContentFiles = new()
            };

            foreach (IPluginParameterInfo parameterInfo in pluginInfo.Parameters)
            {
                pluginInformation.Parameters.Add(new PluginInformation.ParameterInfo()
                {
                    Name = parameterInfo.Name,
                    Description = parameterInfo.Description,
                    Type = parameterInfo.Type.ToString(),
                    Value = parameterInfo.Value?.ToString() ?? null,
                    IsRequired = parameterInfo.IsRequired,
                    IsSecret = parameterInfo.IsSecret
                });
            }

            foreach (IContentFileInfo contentFileInfo in pluginInfo.ContentFiles)
            {
                pluginInformation.ContentFiles.Add(new PluginInformation.ContentFileInfo()
                {
                    Name = contentFileInfo.Name,
                    Length = contentFileInfo.Length
                });
            }

            return Ok(pluginInformation);
        }

        [HttpPost("execute-plugin")]
        public async ValueTask<IActionResult> ExecutePlugin(IFormFile formFile, [FromForm] string parametersJson)
        {
            using Stream stream = formFile.OpenReadStream();
            Stopwatch stopwatch = Stopwatch.StartNew();
            IPluginResult pluginResult = await pluginManager.ExecutePluginAsync(stream, parametersJson);
            stopwatch.Stop();

            JObject jObject = new();
            jObject["PluginResult"] = JObject.FromObject(pluginResult);
            jObject.Add("ExecutionTime", stopwatch.ElapsedMilliseconds);
            
            return Content(jObject.ToString(), "application/json");
        }
    }
}
