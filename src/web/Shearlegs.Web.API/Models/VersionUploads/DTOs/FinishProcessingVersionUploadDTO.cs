using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.VersionUploads.DTOs
{
    public class FinishProcessingVersionUploadDTO
    {
        public int VersionUploadId { get; set; }
        public string PackageId { get; set; }
        public string PackageVersion { get; set; }
        public string ErrorMessage { get; set; }
        public VersionUploadStatus Status { get; set; }
        public string ParametersJson { get; set; }
    }
}
