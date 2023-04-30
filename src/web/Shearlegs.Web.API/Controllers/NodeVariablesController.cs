using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.NodeVariables;
using Shearlegs.Web.API.Models.NodeVariables.Exceptions;
using Shearlegs.Web.API.Models.NodeVariableUserAuthentications.Params;
using Shearlegs.Web.API.Models.UserAuthentications.Exceptions;
using Shearlegs.Web.API.Services.Coordinations.NodeVariableUserAuthentications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("nodevariables")]
    public class NodeVariablesController : RESTFulController
    {
        private readonly INodeVariableUserAuthenticationCoordinationService nodeVariableService;

        public NodeVariablesController(INodeVariableUserAuthenticationCoordinationService nodeVariableService)
        {
            this.nodeVariableService = nodeVariableService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetNodeVariables()
        {
            IEnumerable<NodeVariable> nodeVariables = await nodeVariableService.RetrieveAllNodeVariablesAsync();
            
            return Ok(nodeVariables);
        }

        [HttpGet("{nodeVariableId}")]
        public async ValueTask<IActionResult> GetNodeVariableById(int nodeVariableId)
        {
            try
            {
                NodeVariable nodeVariable = await nodeVariableService.RetrieveNodeVariableByIdAsync(nodeVariableId);

                return Ok(nodeVariable);
            } catch (NotFoundNodeVariableException exception)
            {
                return NotFound(exception);
            }            
        }

        [HttpPost("{nodeVariableId}/update")]
        public async ValueTask<IActionResult> UpdateNodeVariable(int nodeVariableId, UpdateUserNodeVariableParams @params)
        {
            try
            {
                @params.NodeVariableId = nodeVariableId;
                NodeVariable nodeVariable = await nodeVariableService.UpdateUserNodeVariableAsync(@params);

                return Ok(nodeVariable);
            } catch (NotFoundNodeVariableException exception)
            {
                return NotFound(exception);
            } catch (AlreadyExistsNodeVariableException exception)
            {
                return Conflict(exception);
            } catch (NotAuthenticatedUserException exception)
            {
                return Unauthorized(exception);
            }
        }
    }
}
