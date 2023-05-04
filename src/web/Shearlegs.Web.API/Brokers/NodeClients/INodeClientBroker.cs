using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.NodeClient.Models;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.NodeClients
{
    public interface INodeClientBroker
    {
        ValueTask<NodeFullInfo> GetNodeFullInfoAsync(NodeCommunicationDetails nodeCommunicationDetails);
        ValueTask<NodeInfo> GetNodeInfoAsync(NodeCommunicationDetails nodeCommunicationDetails);
    }
}