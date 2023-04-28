using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Params;
using Shearlegs.Web.API.Models.Nodes.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<AddNodeResult> AddNodeAsync(AddNodeParams @params);
        ValueTask<StoredProcedureResult> UpdateNodeAsync(UpdateNodeParams @params);
        ValueTask<Node> GetNodeAsync(GetNodesParams @params);
        ValueTask<IEnumerable<Node>> GetNodesAsync(GetNodesParams @params);
    }
}
