using Shearlegs.Web.APIClient.Models.Nodes;
using Shearlegs.Web.APIClient.Models.Nodes.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Services.NodesAPI
{
    public class NodesAPIService
    {
        private readonly ShearlegsWebAPIClient client;

        public NodesAPIService(ShearlegsWebAPIClient client)
        {
            this.client = client;
        }
        
        public async ValueTask<List<Node>> GetAllNodesAsync()
        {
            string requestUri = "/nodes";

            return await client.GetFromJsonAsync<List<Node>>(requestUri);
        }

        public async ValueTask<Node> GetNodeByIdAsync(int nodeId)
        {
            string requestUri = $"/nodes/{nodeId}";

            return await client.GetFromJsonAsync<Node>(requestUri);
        }

        public async ValueTask<Node> GetNodeByNameAsync(string nodeName)
        {
            string requestUri = $"/nodes/name/{nodeName}";

            return await client.GetFromJsonAsync<Node>(requestUri);
        }

        public async ValueTask<Node> CreateNodeAsync(CreateNodeRequest request)
        {
            string requestUri = "/nodes/create";

            return await client.PostAsJsonAsync<Node>(requestUri, request);
        }

        public async ValueTask<Node> UpdateNodeAsync(int nodeId, UpdateNodeRequest request)
        {
            string requestUri = $"/nodes/{nodeId}/update";

            return await client.PostAsJsonAsync<Node>(requestUri, request);
        }
    }
}
