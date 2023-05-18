using Newtonsoft.Json.Linq;

namespace Shearlegs.Web.API.Models.NodeDaemons
{
    public class ExecutePluginResult
    {
        public JToken PluginResult { get; set; }
        public int ExecutionTime { get; set; }
    }
}
