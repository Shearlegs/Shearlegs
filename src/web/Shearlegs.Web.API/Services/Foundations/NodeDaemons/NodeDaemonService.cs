using Shearlegs.Web.API.Brokers.NodeClients;
using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.NodeClient.Models;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.NodeDaemons
{
    public class NodeDaemonService : INodeDaemonService
    {
        private readonly INodeClientBroker nodeClientBroker;

        public NodeDaemonService(INodeClientBroker nodeClientBroker)
        {
            this.nodeClientBroker = nodeClientBroker;
        }

        public async ValueTask<NodeDaemon> RetrieveNodeDaemonAsync(NodeCommunicationDetails communicationDetails)
        {
            NodeFullInfo nodeFullInfo = await nodeClientBroker.GetNodeFullInfoAsync(communicationDetails);
            
            return new NodeDaemon
            {
                NodeVersion = nodeFullInfo.NodeVersion,
                ShearlegsRuntimeVersion = nodeFullInfo.ShearlegsRuntimeVersion,
                CacheSizeBytes = nodeFullInfo.CacheSizeBytes
            };
        }

        public async ValueTask<NodeDaemonInfo> RetrieveNodeDaemonInfoAsync(NodeCommunicationDetails communicationDetails)
        {
            NodeInfo nodeInfo = await nodeClientBroker.GetNodeInfoAsync(communicationDetails);

            return new NodeDaemonInfo
            {
                NodeVersion = nodeInfo.NodeVersion,
                ShearlegsRuntimeVersion = nodeInfo.ShearlegsRuntimeVersion
            };
        }
    }
}
