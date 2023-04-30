using Shearlegs.Web.API.Models.NodeVariables;
using Shearlegs.Web.API.Models.NodeVariables.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.NodeVariables
{
    public interface INodeVariableService
    {
        ValueTask<NodeVariable> AddNodeVariableAsync(AddNodeVariableParams @params);
        ValueTask<IEnumerable<NodeVariable>> RetrieveAllNodeVariablesAsync();
        ValueTask<NodeVariable> RetrieveNodeVariableByIdAsync(int nodeVariableId);
        ValueTask<NodeVariable> RetrieveNodeVariableByNodeIdAndNameAsync(int nodeId, string variableName);
        ValueTask<IEnumerable<NodeVariable>> RetrieveNodeVariablesByNodeIdAsync(int nodeId);
        ValueTask<NodeVariable> UpdateNodeVariableAsync(UpdateNodeVariableParams @params);
    }
}