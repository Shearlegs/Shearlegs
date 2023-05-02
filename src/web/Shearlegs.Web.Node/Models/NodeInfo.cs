using Microsoft.AspNetCore.Mvc.Formatters;

namespace Shearlegs.Web.Node.Models
{
    public class NodeInfo
    {
        public string NodeVersion { get; set; }
        public string ShearlegsRuntimeVersion { get; set; }
    }
}
