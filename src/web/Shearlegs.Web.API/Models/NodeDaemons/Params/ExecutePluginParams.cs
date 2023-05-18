using Microsoft.AspNetCore.Http;

namespace Shearlegs.Web.API.Models.NodeDaemons.Params
{
    public class ExecutePluginParams
    {
        public IFormFile PluginFile { get; set; }
        public string ParametersJson { get; set; }
    }
}
