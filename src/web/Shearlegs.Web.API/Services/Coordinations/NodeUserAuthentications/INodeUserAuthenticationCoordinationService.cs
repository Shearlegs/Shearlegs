using Shearlegs.Web.API.Models.NodeUserAuthentications.Params;
using Shearlegs.Web.API.Models.Nodes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Coordinations.NodeUserAuthentications
{
    public interface INodeUserAuthenticationCoordinationService
    {
        ValueTask<Node> CreateUserNodeAsync(CreateUserNodeParams @params);
        ValueTask<IEnumerable<Node>> RetrieveAllNodesAsync();
        ValueTask<Node> RetrieveNodeByIdAsync(int nodeId);
        ValueTask<Node> RetrieveNodeByNameAsync(string nodeName);
        ValueTask<Node> UpdateUserNodeAsync(UpdateUserNodeParams @params);
    }
}