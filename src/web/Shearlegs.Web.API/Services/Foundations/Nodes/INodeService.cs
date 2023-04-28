using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Nodes
{
    public interface INodeService
    {
        ValueTask<Node> AddNodeAsync(AddNodeParams @params);
        ValueTask<IEnumerable<Node>> RetrieveAllNodesAsync();
        ValueTask<Node> RetrieveNodeByIdAsync(int nodeId);
        ValueTask<Node> RetrieveNodeByNameAsync(string nodeName);
        ValueTask<Node> UpdateNodeAsync(UpdateNodeParams @params);
    }
}