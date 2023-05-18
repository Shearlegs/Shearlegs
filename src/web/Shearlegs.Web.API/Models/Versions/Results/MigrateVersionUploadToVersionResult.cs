using Shearlegs.Web.API.Utilities.StoredProcedures;

namespace Shearlegs.Web.API.Models.Versions.Results
{
    public class MigrateVersionUploadToVersionResult
    {
        public StoredProcedureResult StoredProcedureResult { get; set; }
        public int? VersionId { get; set; }
    }
}
