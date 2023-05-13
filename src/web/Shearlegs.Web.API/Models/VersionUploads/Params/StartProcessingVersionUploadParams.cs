using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.VersionUploads.Params
{
    public class StartProcessingVersionUploadParams
    {
        public int VersionUploadId { get; set; }
        public int NodeId { get; set;}
        public VersionUploadStatus Status { get; set; }
    }
}
