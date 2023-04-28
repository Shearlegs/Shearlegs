using Shearlegs.Web.API.Models.NodeUserAuthentications.Params;
using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Params;
using Shearlegs.Web.API.Models.UserAuthentications;
using Shearlegs.Web.API.Services.Orchestrations.Nodes;
using Shearlegs.Web.API.Services.Orchestrations.UserAuthentications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Coordinations.NodeUserAuthentications
{
    public class NodeUserAuthenticationCoordinationService : INodeUserAuthenticationCoordinationService
    {
        private readonly INodeOrchestrationService nodeService;
        private readonly IUserAuthenticationOrchestrationService userAuthenticationService;

        public NodeUserAuthenticationCoordinationService(
            INodeOrchestrationService nodeService, 
            IUserAuthenticationOrchestrationService userAuthenticationService)
        {
            this.nodeService = nodeService;
            this.userAuthenticationService = userAuthenticationService;
        }

        public async ValueTask<Node> CreateUserNodeAsync(CreateUserNodeParams @params)
        {
            AuthenticatedUser authenticatedUser = await userAuthenticationService.RetrieveAuthenticatedUserAsync();

            AddNodeParams @addNodeParams = new()
            {
                Name = @params.Name,
                Description = @params.Description,
                FQDN = @params.FQDN,
                Scheme = @params.Scheme,
                IsBehindProxy = @params.IsBehindProxy,
                IsEnabled = @params.IsEnabled,
                CreateUserId = authenticatedUser.User.Id
            };

            return await nodeService.AddNodeAsync(@addNodeParams);
        }

        public async ValueTask<Node> UpdateUserNodeAsync(UpdateUserNodeParams @params)
        {
            AuthenticatedUser authenticatedUser = await userAuthenticationService.RetrieveAuthenticatedUserAsync();

            UpdateNodeParams @updateNodeParams = new()
            {
                NodeId = @params.NodeId,
                Name = @params.Name,
                Description = @params.Description,
                FQDN = @params.FQDN,
                Scheme = @params.Scheme,
                IsBehindProxy = @params.IsBehindProxy,
                IsEnabled = @params.IsEnabled,
                UpdateUserId = authenticatedUser.User.Id
            };

            return await nodeService.UpdateNodeAsync(@updateNodeParams);
        }

        public async ValueTask<Node> RetrieveNodeByIdAsync(int nodeId)
        {
            return await nodeService.RetrieveNodeByIdAsync(nodeId);
        }

        public async ValueTask<Node> RetrieveNodeByNameAsync(string nodeName)
        {
            return await nodeService.RetrieveNodeByNameAsync(nodeName);
        }

        public async ValueTask<IEnumerable<Node>> RetrieveAllNodesAsync()
        {
            return await nodeService.RetrieveAllNodesAsync();
        }
    }
}
