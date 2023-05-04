using Shearlegs.Web.APIClient.Models.Nodes;
using Shearlegs.Web.APIClient.Models.Nodes.Requests;
using Shearlegs.Web.APIClient.Models.NodeVariables;
using Shearlegs.Web.APIClient.Models.NodeVariables.Requests;
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

        // Node Variables

        public async ValueTask<List<NodeVariable>> GetNodeVariablesAsync(int nodeId)
        {
            string requestUri = $"/nodes/{nodeId}/variables";

            return await client.GetFromJsonAsync<List<NodeVariable>>(requestUri);
        }

        public async ValueTask<NodeVariable> GetNodeVariableByNameAsync(int nodeId, string variableName)
        {
            string requestUri = $"/nodes/{nodeId}/variables/name/{variableName}";

            return await client.GetFromJsonAsync<NodeVariable>(requestUri);
        }

        public async ValueTask<NodeVariable> AddNodeVariableAsync(int nodeId, AddNodeVariableRequest request)
        {
            string requestUri = $"/nodes/{nodeId}/variables/add";

            return await client.PostAsJsonAsync<NodeVariable>(requestUri, request);
        }

        // Node Daemon

        public async ValueTask<NodeDaemon> GetNodeDaemonAsync(int nodeId)
        {
            string requestUri = $"/nodes/{nodeId}/daemon";

            return await client.GetFromJsonAsync<NodeDaemon>(requestUri);
        }

        public async ValueTask<NodeDaemonInfo> GetNodeDaemonInfoAsync(int nodeId)
        {
            string requestUri = $"/nodes/{nodeId}/daemon/info";

            return await client.GetFromJsonAsync<NodeDaemonInfo>(requestUri);
        }
    }
}
