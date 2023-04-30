using Shearlegs.Web.API.Models.NodeVariables;
using Shearlegs.Web.API.Models.NodeVariableUserAuthentications.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Coordinations.NodeVariableUserAuthentications
{
    public interface INodeVariableUserAuthenticationCoordinationService
    {
        ValueTask<NodeVariable> AddUserNodeVariableAsync(AddUserNodeVariableParams @params);
        ValueTask<IEnumerable<NodeVariable>> RetrieveAllNodeVariablesAsync();
        ValueTask<NodeVariable> RetrieveNodeVariableByIdAsync(int nodeVariableId);
        ValueTask<NodeVariable> RetrieveNodeVariableByNodeIdAndNameAsync(int nodeId, string variableName);
        ValueTask<IEnumerable<NodeVariable>> RetrieveNodeVariablesByNodeIdAsync(int nodeId);
        ValueTask<NodeVariable> UpdateUserNodeVariableAsync(UpdateUserNodeVariableParams @params);
    }
}