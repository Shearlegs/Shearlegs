using Shearlegs.Web.API.Models.NodeVariables;
using Shearlegs.Web.API.Models.NodeVariables.Params;
using Shearlegs.Web.API.Services.Foundations.NodeVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.NodeVariables
{
    public class NodeVariableOrchestrationService : INodeVariableOrchestrationService
    {
        private readonly INodeVariableService nodeVariableService;

        public NodeVariableOrchestrationService(INodeVariableService nodeVariableService)
        {
            this.nodeVariableService = nodeVariableService;
        }

        public async ValueTask<IEnumerable<NodeVariable>> RetrieveAllNodeVariablesAsync()
        {
            return await nodeVariableService.RetrieveAllNodeVariablesAsync();
        }

        public async ValueTask<NodeVariable> RetrieveNodeVariableByIdAsync(int nodeVariableId)
        {
            return await nodeVariableService.RetrieveNodeVariableByIdAsync(nodeVariableId);
        }

        public async ValueTask<IEnumerable<NodeVariable>> RetrieveNodeVariablesByNodeIdAsync(int nodeId)
        {
            return await nodeVariableService.RetrieveNodeVariablesByNodeIdAsync(nodeId);
        }

        public async ValueTask<NodeVariable> AddNodeVariableAsync(AddNodeVariableParams @params)
        {
            return await nodeVariableService.AddNodeVariableAsync(@params);
        }

        public async ValueTask<NodeVariable> UpdateNodeVariableAsync(UpdateNodeVariableParams @params)
        {
            return await nodeVariableService.UpdateNodeVariableAsync(@params);
        }
    }
}
