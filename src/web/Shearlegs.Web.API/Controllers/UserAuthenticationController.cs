using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.UserAuthentications;
using Shearlegs.Web.API.Models.UserAuthentications.Exceptions;
using Shearlegs.Web.API.Models.UserAuthentications.Params;
using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Models.UserSessions.Exceptions;
using Shearlegs.Web.API.Services.Orchestrations.UserAuthentications;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("users/authentication")]
    public class UserAuthenticationController : RESTFulController
    {
        private readonly IUserAuthenticationOrchestrationService userAuthenticationService;

        public UserAuthenticationController(IUserAuthenticationOrchestrationService userAuthenticationService)
        {
            this.userAuthenticationService = userAuthenticationService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> UserAuthentication()
        {
            try
            {
                AuthenticatedUser authenticatedUser = await userAuthenticationService.RetrieveAuthenticatedUserAsync();

                return Ok(authenticatedUser);
            } catch (NotAuthenticatedUserException exception)
            {
                return Unauthorized(exception);
            }            
        }

        [HttpPost("login")]
        public async ValueTask<IActionResult> Login([FromBody] LoginUserWithPasswordParams @params)
        {
            try
            {
                UserAuthenticationToken token = await userAuthenticationService.LoginUserWithPasswordAsync(@params);

                return Ok(token);
            } catch (InvalidPasswordUserException exception)
            {
                return BadRequest(exception);
            } catch (NotFoundUserException exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost("logout")]
        public async ValueTask<IActionResult> Logout()
        {
            try
            {
                await userAuthenticationService.LogoutUserAsync();

                return Ok();
            } catch (NotFoundUserSessionException exception)
            {
                return NotFound(exception);
            }
        }
    }
}
