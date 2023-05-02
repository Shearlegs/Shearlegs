using Shearlegs.Web.APIClient.Models.NodeVariables;
using Shearlegs.Web.APIClient.Models.NodeVariables.Requests;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Services.NodeVariablesAPI
{
    public class NodeVariablesAPIService
    {
        private readonly ShearlegsWebAPIClient client;

        public NodeVariablesAPIService(ShearlegsWebAPIClient client)
        {
            this.client = client;
        }

        public async ValueTask<NodeVariable> UpdateNodeVariableAsync(int nodeVariableId, UpdateNodeVariableRequest request)
        {
            string requestUri = $"/nodevariables/{nodeVariableId}/update";

            return await client.PostAsJsonAsync<NodeVariable>(requestUri, request);
        }
    }
}
