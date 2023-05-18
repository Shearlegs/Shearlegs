using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Shearlegs.Web.API.Brokers.NodeClients;
using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.API.Models.NodeDaemons.Exceptions;
using Shearlegs.Web.API.Models.NodeDaemons.Params;
using Shearlegs.Web.NodeClient.Models;
using Shearlegs.Web.NodeClient.Models.Exceptions;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.NodeDaemons
{
    public partial class NodeDaemonService : INodeDaemonService
    {
        private readonly INodeClientBroker nodeClientBroker;

        public NodeDaemonService(INodeClientBroker nodeClientBroker)
        {
            this.nodeClientBroker = nodeClientBroker;
        }

        public async ValueTask<NodeDaemonStatistics> RetrieveNodeDaemonAsync(NodeCommunicationDetails communicationDetails)
        {
            try
            {
                NodeStatistics nodeStats = await nodeClientBroker.GetNodeStatisticsAsync(communicationDetails);

                return new NodeDaemonStatistics
                {
                    CacheSizeBytes = nodeStats.CacheSizeBytes
                };
            }
            catch (ShearlegsWebNodeClientRequestException exception)
            {
                throw new NodeDaemonCommunicationException(exception);
            }
        }

        public async ValueTask<NodeDaemonInfo> RetrieveNodeDaemonInfoAsync(NodeCommunicationDetails communicationDetails)
        {
            try
            {
                NodeInfo nodeInfo = await nodeClientBroker.GetNodeInfoAsync(communicationDetails);

                return new NodeDaemonInfo
                {
                    NodeVersion = nodeInfo.NodeVersion,
                    ShearlegsRuntimeVersion = nodeInfo.ShearlegsRuntimeVersion
                };
            } catch (ShearlegsWebNodeClientRequestException exception)
            {
                throw new NodeDaemonCommunicationException(exception);
            }
        }

        public async ValueTask<ProcessedPluginInfo> ProcessPluginAsync(NodeCommunicationDetails communicationDetails, ProcessPluginParams @params)
        {
            try
            {
                PluginInformation pluginInformation = await nodeClientBroker.ProcessPluginAsync(communicationDetails, @params);

                return MapPluginInformationToProcessedPluginInfo(pluginInformation);
            } catch (ShearlegsWebNodeClientRequestException exception)
            {
                throw new NodeDaemonCommunicationException(exception);
            }
        }

        public async ValueTask<ExecutePluginResult> ExecutePluginAsync(NodeCommunicationDetails communicationDetails, ExecutePluginParams @params)
        {
            try
            {
                string result = await nodeClientBroker.ExecutePluginAsync(communicationDetails, @params);

                JObject jObject = JObject.Parse(result);

                return new ExecutePluginResult
                {
                    PluginResult = jObject["PluginResult"],
                    ExecutionTime = jObject["ExecutionTime"].Value<int>()
                };
            }
            catch (ShearlegsWebNodeClientRequestException exception)
            {
                throw new NodeDaemonCommunicationException(exception);
            }
        }
    }
}
