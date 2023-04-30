using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Models.Nodes.Exceptions;
using Shearlegs.Web.API.Models.NodeVariables;
using Shearlegs.Web.API.Models.NodeVariables.Exceptions;
using Shearlegs.Web.API.Models.NodeVariables.Params;
using Shearlegs.Web.API.Models.NodeVariables.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.NodeVariables
{
    public class NodeVariableService : INodeVariableService
    {
        private readonly IStorageBroker storageBroker;

        public NodeVariableService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<IEnumerable<NodeVariable>> RetrieveAllNodeVariablesAsync()
        {
            GetNodeVariablesParams @params = new();

            return await storageBroker.GetNodeVariablesAsync(@params);
        }

        public async ValueTask<NodeVariable> RetrieveNodeVariableByIdAsync(int nodeVariableId)
        {
            GetNodeVariablesParams @params = new()
            {
                NodeVariableId = nodeVariableId
            };

            NodeVariable nodeVariable = await storageBroker.GetNodeVariableAsync(@params);

            if (nodeVariable == null)
            {
                throw new NotFoundNodeVariableException();
            }

            return nodeVariable;
        }

        public async ValueTask<IEnumerable<NodeVariable>> RetrieveNodeVariablesByNodeIdAsync(int nodeId)
        {
            GetNodeVariablesParams @params = new()
            {
                NodeId = nodeId
            };

            return await storageBroker.GetNodeVariablesAsync(@params);
        }

        public async ValueTask<NodeVariable> RetrieveNodeVariableByNodeIdAndNameAsync(int nodeId, string variableName)
        {
            GetNodeVariablesParams @params = new()
            {
                NodeId = nodeId,
                Name = variableName
            };

            NodeVariable nodeVariable = await storageBroker.GetNodeVariableAsync(@params);

            if (nodeVariable == null)
            {
                throw new NotFoundNodeVariableException();
            }

            return nodeVariable;
        }

        public async ValueTask<NodeVariable> AddNodeVariableAsync(AddNodeVariableParams @params)
        {
            AddNodeVariableResult result = await storageBroker.AddNodeVariableAsync(@params);

            if (result.StoredProcedureResult.ReturnValue == 1)
            {
                throw new NotFoundNodeException();
            }

            if (result.StoredProcedureResult.ReturnValue == 2)
            {
                throw new AlreadyExistsNodeVariableException();
            }

            NodeVariable nodeVariable = await RetrieveNodeVariableByIdAsync(result.NodeVariableId.GetValueOrDefault());

            return nodeVariable;
        }

        public async ValueTask<NodeVariable> UpdateNodeVariableAsync(UpdateNodeVariableParams @params)
        {
            StoredProcedureResult storedProcedureResult = await storageBroker.UpdateNodeVariableAsync(@params);

            if (storedProcedureResult.ReturnValue == 1)
            {
                throw new NotFoundNodeVariableException();
            }

            if (storedProcedureResult.ReturnValue == 2)
            {
                throw new AlreadyExistsNodeVariableException();
            }

            NodeVariable nodeVariable = await RetrieveNodeVariableByIdAsync(@params.NodeVariableId);

            return nodeVariable;
        }
    }
}
