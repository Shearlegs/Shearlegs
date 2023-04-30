using Shearlegs.Web.API.Models.NodeVariables;
using Shearlegs.Web.API.Models.NodeVariables.Params;
using Shearlegs.Web.API.Models.NodeVariableUserAuthentications.Params;
using Shearlegs.Web.API.Models.UserAuthentications;
using Shearlegs.Web.API.Services.Orchestrations.NodeVariables;
using Shearlegs.Web.API.Services.Orchestrations.UserAuthentications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Coordinations.NodeVariableUserAuthentications
{
    public class NodeVariableUserAuthenticationCoordinationService : INodeVariableUserAuthenticationCoordinationService
    {
        private readonly INodeVariableOrchestrationService nodeVariableService;
        private readonly IUserAuthenticationOrchestrationService userAuthenticationService;

        public NodeVariableUserAuthenticationCoordinationService(
            INodeVariableOrchestrationService nodeVariableService, 
            IUserAuthenticationOrchestrationService userAuthenticationService)
        {
            this.nodeVariableService = nodeVariableService;
            this.userAuthenticationService = userAuthenticationService;
        }

        public async ValueTask<NodeVariable> AddUserNodeVariableAsync(AddUserNodeVariableParams @params)
        {
            AuthenticatedUser authenticatedUser = await userAuthenticationService.RetrieveAuthenticatedUserAsync();

            AddNodeVariableParams @addNodeVariableParams = new()
            {
                NodeId = @params.NodeId,
                Name = @params.Name,
                Value = @params.Value,
                Description = @params.Description,
                IsSensitive = @params.IsSensitive,
                CreateUserId = authenticatedUser.User.Id                
            };

            return await nodeVariableService.AddNodeVariableAsync(addNodeVariableParams);
        }

        public async ValueTask<NodeVariable> UpdateUserNodeVariableAsync(UpdateUserNodeVariableParams @params)
        {
            AuthenticatedUser authenticatedUser = await userAuthenticationService.RetrieveAuthenticatedUserAsync();

            UpdateNodeVariableParams @updateNodeVariableParams = new()
            {
                NodeVariableId = @params.NodeVariableId,
                Name = @params.Name,
                Value = @params.Value,
                Description = @params.Description,
                IsSensitive = @params.IsSensitive,
                UpdateUserId = authenticatedUser.User.Id
            };

            return await nodeVariableService.UpdateNodeVariableAsync(@updateNodeVariableParams);
        }

        public async ValueTask<NodeVariable> RetrieveNodeVariableByIdAsync(int nodeVariableId)
        {
            return await nodeVariableService.RetrieveNodeVariableByIdAsync(nodeVariableId);
        }

        public async ValueTask<IEnumerable<NodeVariable>> RetrieveAllNodeVariablesAsync()
        {
            return await nodeVariableService.RetrieveAllNodeVariablesAsync();
        }

        public async ValueTask<IEnumerable<NodeVariable>> RetrieveNodeVariablesByNodeIdAsync(int nodeId)
        {
            return await nodeVariableService.RetrieveNodeVariablesByNodeIdAsync(nodeId);
        }

        public async ValueTask<NodeVariable> RetrieveNodeVariableByNodeIdAndNameAsync(int nodeId, string variableName)
        {
            return await nodeVariableService.RetrieveNodeVariableByNodeIdAndNameAsync(nodeId, variableName);
        }
    }
}
