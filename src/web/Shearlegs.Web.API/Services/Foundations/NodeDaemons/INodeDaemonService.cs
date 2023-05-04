using Shearlegs.Web.API.Models.NodeDaemons;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.NodeDaemons
{
    public interface INodeDaemonService
    {
        ValueTask<NodeDaemon> RetrieveNodeDaemonAsync(NodeCommunicationDetails communicationDetails);
        ValueTask<NodeDaemonInfo> RetrieveNodeDaemonInfoAsync(NodeCommunicationDetails communicationDetails);
    }
}