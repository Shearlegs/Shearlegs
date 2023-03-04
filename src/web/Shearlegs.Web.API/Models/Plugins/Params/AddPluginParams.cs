using System.Xml.Linq;

namespace Shearlegs.Web.API.Models.Plugins.Params
{
    public class AddPluginParams
	{
        public string PackageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int? CreateUserId { get; set; }
    }
}
