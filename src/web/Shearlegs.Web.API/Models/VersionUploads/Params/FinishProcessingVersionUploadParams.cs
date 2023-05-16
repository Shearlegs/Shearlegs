using Shearlegs.Web.Shared.Enums;
using System.Collections.Generic;

namespace Shearlegs.Web.API.Models.VersionUploads.Params
{
    public class FinishProcessingVersionUploadParams
    {
        public int VersionUploadId { get; set; }
        public string PackageId { get; set; }
        public string PackageVersion { get; set; }
        public string ErrorMessage { get; set; }
        public VersionUploadStatus Status { get; set; }
        public List<Parameter> Parameters { get; set; }

        public class Parameter
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string DataType { get; set; }
            public string DefaultValue { get; set; }
            public bool IsArray { get; set; }
            public bool IsRequired { get; set; }
            public bool IsSecret { get; set; }
        }
    }
}
