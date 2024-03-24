using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.API.Models.NodeDaemons.Params;
using Shearlegs.Web.NodeClient;
using Shearlegs.Web.NodeClient.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.NodeClients
{
    public class NodeClientBroker : INodeClientBroker
    {
        private readonly IHttpClientFactory httpClientFactory;

        public NodeClientBroker(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async ValueTask<NodeInfo> GetNodeInfoAsync(NodeCommunicationDetails nodeCommunicationDetails)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(nodeCommunicationDetails.GetBaseAddress());

            ShearlegsWebNodeClient nodeClient = new(httpClient);

            return await nodeClient.GetNodeInfoAsync();
        }

        public async ValueTask<NodeStatistics> GetNodeStatisticsAsync(NodeCommunicationDetails nodeCommunicationDetails)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(nodeCommunicationDetails.GetBaseAddress());

            ShearlegsWebNodeClient nodeClient = new(httpClient);

            return await nodeClient.GetNodeStatisticsAsync();
        }

        public async ValueTask<PluginInformation> ProcessPluginAsync(NodeCommunicationDetails nodeCommunicationDetails, ProcessPluginParams @params)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(nodeCommunicationDetails.GetBaseAddress());

            ShearlegsWebNodeClient nodeClient = new(httpClient);

            return await nodeClient.ProcessPluginAsync(@params.PluginFile);
        }

        public async ValueTask<string> ExecutePluginAsync(NodeCommunicationDetails nodeCommunicationDetails, ExecutePluginParams @params)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(nodeCommunicationDetails.GetBaseAddress());

            ShearlegsWebNodeClient nodeClient = new(httpClient);

            return await nodeClient.ExecutePluginAsync(@params.PluginFile, @params.ParametersJson);            
        }
    }
}
