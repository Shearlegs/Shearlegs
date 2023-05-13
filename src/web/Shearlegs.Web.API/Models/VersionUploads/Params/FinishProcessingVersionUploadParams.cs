using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.VersionUploads.Params
{
    public class FinishProcessingVersionUploadParams
    {
        public int VersionUploadId { get; set; }
        public string PackageId { get; set; }
        public string PackageVersion { get; set; }
        public string ErrorMessage { get; set; }
        public VersionUploadStatus Status { get; set; }
    }
}
