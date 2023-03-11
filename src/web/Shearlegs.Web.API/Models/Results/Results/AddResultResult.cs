using Shearlegs.Web.API.Utilities.StoredProcedures;

namespace Shearlegs.Web.API.Models.Results.Results
{
    public class AddResultResult
    {
        public StoredProcedureResult StoredProcedureResult { get; set; }
        public int? ResultId { get; set; }
    }
}
