using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.API.Models.NodeDaemons.Params;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.NodeDaemons
{
    public interface INodeDaemonService
    {
        ValueTask<ProcessedPluginInfo> ProcessPluginAsync(NodeCommunicationDetails communicationDetails, ProcessPluginParams @params);
        ValueTask<NodeDaemonStatistics> RetrieveNodeDaemonAsync(NodeCommunicationDetails communicationDetails);
        ValueTask<NodeDaemonInfo> RetrieveNodeDaemonInfoAsync(NodeCommunicationDetails communicationDetails);
    }
}