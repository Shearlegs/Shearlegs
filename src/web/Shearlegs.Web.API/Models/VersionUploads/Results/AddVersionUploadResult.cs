using Shearlegs.Web.API.Utilities.StoredProcedures;

namespace Shearlegs.Web.API.Models.VersionUploads.Results
{
    public class AddVersionUploadResult
    {
        public StoredProcedureResult StoredProcedureResult { get; set; }
        public int? VersionUploadId { get; set; }
    }
}
