using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Params;
using Shearlegs.Web.API.Services.Foundations.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Nodes
{
    public class NodeOrchestrationService : INodeOrchestrationService
    {
        private readonly INodeService nodeService;

        public NodeOrchestrationService(INodeService nodeService)
        {
            this.nodeService = nodeService;
        }

        public async ValueTask<Node> AddNodeAsync(AddNodeParams @params)
        {
            return await nodeService.AddNodeAsync(@params);
        }

        public async ValueTask<IEnumerable<Node>> RetrieveAllNodesAsync()
        {
            return await nodeService.RetrieveAllNodesAsync();
        }

        public async ValueTask<Node> RetrieveNodeByIdAsync(int nodeId)
        {
            return await nodeService.RetrieveNodeByIdAsync(nodeId);
        }

        public async ValueTask<Node> RetrieveNodeByNameAsync(string nodeName)
        {
            return await nodeService.RetrieveNodeByNameAsync(nodeName);
        }

        public async ValueTask<Node> UpdateNodeAsync(UpdateNodeParams @params)
        {
            return await nodeService.UpdateNodeAsync(@params);
        }
    }
}
