using System.Collections.Generic;

namespace Shearlegs.Web.API.Models.Versions.Params
{
    public class CreateVersionParams
    {
        public int PluginId { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public int? CreateUserId { get; set; }

        public List<VersionParameter> Parameters { get; set; }

        public class VersionParameter
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string InputType { get; set; }
            public string DataType { get; set; }
            public string DefaultValue { get; set; }
            public bool IsArray { get; set; }
            public bool IsRequired { get; set; }
            public bool IsSecret { get; set; }
        }
    }
}
