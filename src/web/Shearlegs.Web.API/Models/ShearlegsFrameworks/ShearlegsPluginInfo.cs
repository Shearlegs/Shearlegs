using System.Collections.Generic;

namespace Shearlegs.Web.API.Models.ShearlegsFrameworks
{
    public class ShearlegsPluginInfo
    {
        public string PackageId { get; set; }
        public string Version { get; set; }
        public List<ShearlegsPluginParameterInfo> Parameters { get; set; }
    }
}
