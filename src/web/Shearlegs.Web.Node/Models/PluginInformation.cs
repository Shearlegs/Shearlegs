using System.Collections.Generic;

namespace Shearlegs.Web.Node.Models
{
    public class PluginInformation
    {
        public string PackageId { get; set; }
        public string Version { get; set; }
        public bool IsPrerelease { get; set; }

        public List<ParameterInfo> Parameters { get; set; }
        public List<ContentFileInfo> ContentFiles { get; set; }

        public class ParameterInfo
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
            public bool IsRequired { get; set; }
            public bool IsSecret { get; set; }
        }

        public class ContentFileInfo
        {
            public string Name { get; set; }
            public long Length { get; set; }
        }
    }
}
