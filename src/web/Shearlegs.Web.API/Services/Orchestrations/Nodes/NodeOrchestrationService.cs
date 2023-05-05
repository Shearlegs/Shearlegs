using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Params;
using Shearlegs.Web.API.Services.Foundations.NodeDaemons;
using Shearlegs.Web.API.Services.Foundations.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Nodes
{
    public class NodeOrchestrationService : INodeOrchestrationService
    {
        private readonly INodeService nodeService;
        private readonly INodeDaemonService nodeDaemonService;

        public NodeOrchestrationService(INodeService nodeService, INodeDaemonService nodeDaemonService)
        {
            this.nodeService = nodeService;
            this.nodeDaemonService = nodeDaemonService;
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

        public async ValueTask<NodeDaemonInfo> RetrieveNodeDaemonInfoByIdAsync(int nodeId)
        {
            Node node = await nodeService.RetrieveNodeByIdAsync(nodeId);

            NodeCommunicationDetails communicationDetails = new()
            {
                Scheme = node.Scheme,
                FQDN = node.FQDN,
                HttpPort = node.HttpPort,
                HttpsPort = node.HttpsPort,
                IsBehindProxy = node.IsBehindProxy
            };

            return await nodeDaemonService.RetrieveNodeDaemonInfoAsync(communicationDetails);
        }

        public async ValueTask<NodeDaemonStatistics> RetrieveNodeDaemonByIdAsync(int nodeId)
        {
            Node node = await nodeService.RetrieveNodeByIdAsync(nodeId);

            NodeCommunicationDetails communicationDetails = new()
            {
                Scheme = node.Scheme,
                FQDN = node.FQDN,
                HttpPort = node.HttpPort,
                HttpsPort = node.HttpsPort,
                IsBehindProxy = node.IsBehindProxy
            };

            return await nodeDaemonService.RetrieveNodeDaemonAsync(communicationDetails);
        }
    }
}
