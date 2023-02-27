using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Models.Users.Params;
using Shearlegs.Web.API.Services.Foundations.Users;
using Shearlegs.Web.API.Services.Processings.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : RESTFulController
    {
        private readonly IUserService userService;
        private readonly IUserProcessingService userProcessingService;

        public UsersController(IUserService userService, IUserProcessingService userProcessingService)
        {
            this.userService = userService;
            this.userProcessingService = userProcessingService;
        }

        [HttpGet("{userId}")]
        public async ValueTask<IActionResult> GetUserById(int userId)
        {
            try
            {
                User user = await userService.RetrieveUserByIdAsync(userId);

                return Ok(user);
            } catch (NotFoundUserException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpGet("name/{username}")]
        public async ValueTask<IActionResult> GetUserByName(string username)
        {
            try
            {
                User user = await userService.RetrieveUserByNameAsync(username);

                return Ok(user);
            }
            catch (NotFoundUserException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllUsers()
        {
            IEnumerable<User> user = await userService.RetrieveAllUsersAsync();

            return Ok(user);
        }

        [HttpPost("login/password")]
        public async ValueTask<IActionResult> GetUserByNameAndPassword([FromBody] GetUserByNameAndPasswordParams @params)
        {
            try
            {
                User user = await userProcessingService.RetrieveUserByNameAndPasswordAsync(@params.Username, @params.Password);

                return Ok(user);
            } catch (NotFoundUserException exception)
            {
                return NotFound(exception);
            } catch (InvalidPasswordUserException exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost("register/password")]
        public async ValueTask<IActionResult> AddUserWithPassword([FromBody] AddUserWithPasswordParams @params)
        {
            try
            {
                User user = await userProcessingService.AddUserWithPasswordAsync(@params);

                return Ok(user);
            } catch (AddUserValidationException exception)
            {
                return BadRequest(exception);
            } catch (AlreadyExistsUserException exception)
            {
                return Conflict(exception);
            }
        }

        [HttpPost("register/windows")]
        public async ValueTask<IActionResult> AddUserWithWindows([FromBody] AddUserWithWindowsParams @params)
        {
            try
            {
                User user = await userProcessingService.AddUserWithWindowsAsync(@params);

                return Ok(user);
            }
            catch (AddUserValidationException exception)
            {
                return BadRequest(exception);
            }
            catch (AlreadyExistsUserException exception)
            {
                return Conflict(exception);
            }
        }
    }
}
