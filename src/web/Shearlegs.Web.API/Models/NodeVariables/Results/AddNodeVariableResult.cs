using Shearlegs.Web.API.Utilities.StoredProcedures;

namespace Shearlegs.Web.API.Models.NodeVariables.Results
{
    public class AddNodeVariableResult
    {
        public StoredProcedureResult StoredProcedureResult { get; set; }
        public int? NodeVariableId { get; set; }
    }
}
