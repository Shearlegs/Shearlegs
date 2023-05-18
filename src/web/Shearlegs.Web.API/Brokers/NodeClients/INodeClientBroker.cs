using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.API.Models.NodeDaemons.Params;
using Shearlegs.Web.NodeClient.Models;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.NodeClients
{
    public interface INodeClientBroker
    {
        ValueTask<NodeStatistics> GetNodeStatisticsAsync(NodeCommunicationDetails nodeCommunicationDetails);
        ValueTask<NodeInfo> GetNodeInfoAsync(NodeCommunicationDetails nodeCommunicationDetails);
        ValueTask<PluginInformation> ProcessPluginAsync(NodeCommunicationDetails nodeCommunicationDetails, ProcessPluginParams @params);
        ValueTask<string> ExecutePluginAsync(NodeCommunicationDetails nodeCommunicationDetails, ExecutePluginParams @params);
    }
}