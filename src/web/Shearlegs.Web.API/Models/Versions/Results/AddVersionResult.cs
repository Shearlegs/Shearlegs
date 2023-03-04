using Shearlegs.Web.API.Utilities.StoredProcedures;

namespace Shearlegs.Web.API.Models.Versions.Results
{
    public class AddVersionResult
    {
        public StoredProcedureResult StoredProcedureResult { get; set; }
        public int? VersionId { get; set; }
    }
}
