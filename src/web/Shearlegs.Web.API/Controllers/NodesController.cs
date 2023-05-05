using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.NodeUserAuthentications.Params;
using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Exceptions;
using Shearlegs.Web.API.Services.Coordinations.NodeUserAuthentications;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shearlegs.Web.API.Services.Coordinations.NodeVariableUserAuthentications;
using Shearlegs.Web.API.Models.NodeVariables;
using Shearlegs.Web.API.Models.NodeVariableUserAuthentications.Params;
using Shearlegs.Web.API.Models.NodeVariables.Exceptions;
using Shearlegs.Web.API.Models.UserAuthentications.Exceptions;
using Shearlegs.Web.API.Models.NodeDaemons;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("nodes")]
    public class NodesController : RESTFulController
    {
        private readonly INodeUserAuthenticationCoordinationService nodeService;
        private readonly INodeVariableUserAuthenticationCoordinationService nodeVariableService;

        public NodesController(
            INodeUserAuthenticationCoordinationService nodeService, 
            INodeVariableUserAuthenticationCoordinationService nodeVariableService)
        {
            this.nodeService = nodeService;
            this.nodeVariableService = nodeVariableService;
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
            } catch (NotAuthenticatedUserException exception)
            {
                return Unauthorized(exception);
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
            } catch (NotAuthenticatedUserException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("{nodeId}/variables")]
        public async ValueTask<IActionResult> GetNodeVariables(int nodeId)
        {
            IEnumerable<NodeVariable> nodeVariables = await nodeVariableService.RetrieveNodeVariablesByNodeIdAsync(nodeId);

            return Ok(nodeVariables);
        }

        [HttpGet("{nodeId}/variables/name/{variableName}")]
        public async ValueTask<IActionResult> GetNodeVariableByName(int nodeId, string variableName)
        {
            try
            {
                NodeVariable nodeVariable = await nodeVariableService.RetrieveNodeVariableByNodeIdAndNameAsync(nodeId, variableName);

                return Ok(nodeVariable);
            } catch (NotFoundNodeVariableException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpPost("{nodeId}/variables/add")]
        public async ValueTask<IActionResult> AddNodeVariable(int nodeId, AddUserNodeVariableParams @params)
        {
            try
            {
                @params.NodeId = nodeId;
                NodeVariable nodeVariable = await nodeVariableService.AddUserNodeVariableAsync(@params);

                return Ok(nodeVariable);
            }
            catch (NotFoundNodeException exception)
            {
                return NotFound(exception);
            }
            catch (AlreadyExistsNodeVariableException exception)
            {
                return Conflict(exception);
            } catch (NotAuthenticatedUserException exception)
            {
                return Unauthorized(exception);
            }
        }

        [HttpGet("{nodeId}/daemon/statistics")]
        public async ValueTask<IActionResult> GetNodeDaemonStatistics(int nodeId)
        {
            try
            {
                NodeDaemonStatistics nodeDaemon = await nodeService.RetrieveNodeDaemonStatisticsByIdAsync(nodeId);

                return Ok(nodeDaemon);
            } catch (NotFoundNodeException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpGet("{nodeId}/daemon/info")]
        public async ValueTask<IActionResult> GetNodeDaemonInfo(int nodeId)
        {
            try
            {
                NodeDaemonInfo nodeDaemonInfo = await nodeService.RetrieveNodeDaemonInfoByIdAsync(nodeId);

                return Ok(nodeDaemonInfo);
            } catch (NotFoundNodeException exception)
            {
                return NotFound(exception);
            }
        }
    }
}
