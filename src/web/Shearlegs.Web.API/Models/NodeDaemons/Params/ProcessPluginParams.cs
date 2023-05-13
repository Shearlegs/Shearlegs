using Microsoft.AspNetCore.Http;

namespace Shearlegs.Web.API.Models.NodeDaemons.Params
{
    public class ProcessPluginParams
    {
        public IFormFile PluginFile { get; set; }
    }
}
