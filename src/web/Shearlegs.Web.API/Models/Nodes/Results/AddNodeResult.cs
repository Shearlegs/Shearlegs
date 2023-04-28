using Shearlegs.Web.API.Utilities.StoredProcedures;

namespace Shearlegs.Web.API.Models.Nodes.Results
{
    public class AddNodeResult
    {
        public StoredProcedureResult StoredProcedureResult { get; set; }
        public int? NodeId { get; set; }
    }
}
