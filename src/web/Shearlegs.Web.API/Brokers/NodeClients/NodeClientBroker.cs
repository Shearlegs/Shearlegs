using Shearlegs.Web.API.Models.NodeDaemons;
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
            HttpClient httpClient = new() 
            {
                BaseAddress = new Uri(nodeCommunicationDetails.GetBaseAddress())
            };
            ShearlegsWebNodeClient nodeClient = new(httpClient);

            return await nodeClient.GetNodeInfoAsync();
        }

        public async ValueTask<NodeFullInfo> GetNodeFullInfoAsync(NodeCommunicationDetails nodeCommunicationDetails)
        {
            HttpClient httpClient = new()
            {
                BaseAddress = new Uri(nodeCommunicationDetails.GetBaseAddress())
            };
            ShearlegsWebNodeClient nodeClient = new(httpClient);

            return await nodeClient.GetNodeFullInfoAsync();
        }
    }
}
