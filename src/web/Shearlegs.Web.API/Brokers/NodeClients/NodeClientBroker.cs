using Shearlegs.Web.NodeClient;
using Shearlegs.Web.NodeClient.Models;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.NodeClients
{
    public class NodeClientBroker : INodeClientBroker
    {
        private readonly ShearlegsWebNodeClient nodeClient;

        public NodeClientBroker(ShearlegsWebNodeClient nodeClient)
        {
            this.nodeClient = nodeClient;
        }

        public async ValueTask<NodeInfo> GetNodeInfoAsync()
        {
            return await nodeClient.GetNodeInfoAsync();
        }

        public async ValueTask<NodeFullInfo> GetNodeFullInfoAsync()
        {
            return await nodeClient.GetNodeFullInfoAsync();
        }
    }
}
