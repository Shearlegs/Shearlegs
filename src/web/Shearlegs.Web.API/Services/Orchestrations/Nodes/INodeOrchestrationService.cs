using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Nodes
{
    public interface INodeOrchestrationService
    {
        ValueTask<Node> AddNodeAsync(AddNodeParams @params);
        ValueTask<IEnumerable<Node>> RetrieveAllNodesAsync();
        ValueTask<Node> RetrieveNodeByIdAsync(int nodeId);
        ValueTask<Node> RetrieveNodeByNameAsync(string nodeName);
        ValueTask<NodeDaemon> RetrieveNodeDaemonByIdAsync(int nodeId);
        ValueTask<NodeDaemonInfo> RetrieveNodeDaemonInfoByIdAsync(int nodeId);
        ValueTask<Node> UpdateNodeAsync(UpdateNodeParams @params);
    }
}