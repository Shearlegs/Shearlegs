using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Exceptions;
using Shearlegs.Web.API.Models.Nodes.Params;
using Shearlegs.Web.API.Models.Nodes.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Nodes
{
    public class NodeService : INodeService
    {
        private readonly IStorageBroker storageBroker;

        public NodeService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<IEnumerable<Node>> RetrieveAllNodesAsync()
        {
            GetNodesParams @params = new();

            return await storageBroker.GetNodesAsync(@params);
        }

        public async ValueTask<Node> RetrieveNodeByIdAsync(int nodeId)
        {
            GetNodesParams @params = new()
            {
                NodeId = nodeId
            };

            Node node = await storageBroker.GetNodeAsync(@params);

            if (node == null)
            {
                throw new NotFoundNodeException();
            }

            return node;
        }

        public async ValueTask<Node> RetrieveNodeByNameAsync(string nodeName)
        {
            GetNodesParams @params = new()
            {
                Name = nodeName
            };

            Node node = await storageBroker.GetNodeAsync(@params);

            if (node == null)
            {
                throw new NotFoundNodeException();
            }

            return node;
        }

        public async ValueTask<Node> AddNodeAsync(AddNodeParams @params)
        {
            AddNodeResult result = await storageBroker.AddNodeAsync(@params);

            if (result.StoredProcedureResult.ReturnValue == 1)
            {
                throw new AlreadyExistsNodeException();
            }

            return await RetrieveNodeByIdAsync(result.NodeId.GetValueOrDefault());
        }

        public async ValueTask<Node> UpdateNodeAsync(UpdateNodeParams @params)
        {
            StoredProcedureResult result = await storageBroker.UpdateNodeAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundNodeException();
            }

            if (result.ReturnValue == 2)
            {
                throw new AlreadyExistsNodeException();
            }

            return await RetrieveNodeByIdAsync(@params.NodeId);
        }
    }
}
