using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.NodeUserAuthentications.Params;
using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Exceptions;
using Shearlegs.Web.API.Services.Coordinations.NodeUserAuthentications;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("nodes")]
    public class NodesController : RESTFulController
    {
        private readonly INodeUserAuthenticationCoordinationService nodeService;

        public NodesController(INodeUserAuthenticationCoordinationService nodeService)
        {
            this.nodeService = nodeService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetNodes()
        {
            IEnumerable<Node> nodes = await nodeService.RetrieveAllNodesAsync();

            return Ok(nodes);
        }

        [HttpGet("{nodeId}")]
        public async ValueTask<IActionResult> GetNodeById(int nodeId)
        {
            try
            {
                Node node = await nodeService.RetrieveNodeByIdAsync(nodeId);

                return Ok(node);
            }
            catch (NotFoundNodeException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpGet("name/{nodeName}")]
        public async ValueTask<IActionResult> GetNodeByName(string nodeName)
        {
            try
            {
                Node node = await nodeService.RetrieveNodeByNameAsync(nodeName);
                return Ok(node);
            }
            catch (NotFoundNodeException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpPost("create")]
        public async ValueTask<IActionResult> CreateUserNode(CreateUserNodeParams @params)
        {
            try
            {
                Node node = await nodeService.CreateUserNodeAsync(@params);

                return Ok(node);
            } catch (AlreadyExistsNodeException exception)
            {
                return Conflict(exception);
            }
        }

        [HttpPost("{nodeId}/update")]
        public async ValueTask<IActionResult> UpdateUserNode(int nodeId, UpdateUserNodeParams @params)
        {
            try
            {
                @params.NodeId = nodeId;
                Node node = await nodeService.UpdateUserNodeAsync(@params);

                return Ok(node);
            } catch (NotFoundNodeException exception)
            {
                return NotFound(exception);
            } catch (AlreadyExistsNodeException exception)
            {
                return Conflict(exception);
            }
        }
    }
}
