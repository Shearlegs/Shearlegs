using Newtonsoft.Json;
using System.Collections.Generic;

namespace Shearlegs.Web.API.Models.Versions.Params
{
    public class AddVersionParams
    {
        public int PluginId { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public long ContentLength { get; set; }
        public int? CreateUserId { get; set; }
        public string ParametersJson { get; set; }
    }
}
