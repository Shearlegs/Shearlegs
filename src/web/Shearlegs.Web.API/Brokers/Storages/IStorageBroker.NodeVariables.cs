using Shearlegs.Web.API.Models.NodeVariables;
using Shearlegs.Web.API.Models.NodeVariables.Params;
using Shearlegs.Web.API.Models.NodeVariables.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<AddNodeVariableResult> AddNodeVariableAsync(AddNodeVariableParams @params);
        ValueTask<StoredProcedureResult> UpdateNodeVariableAsync(UpdateNodeVariableParams @params);
        ValueTask<NodeVariable> GetNodeVariableAsync(GetNodeVariablesParams @params);
        ValueTask<IEnumerable<NodeVariable>> GetNodeVariablesAsync(GetNodeVariablesParams @params);
    }
}
