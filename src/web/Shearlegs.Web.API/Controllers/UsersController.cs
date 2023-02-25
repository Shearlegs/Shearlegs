using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Users.Exceptions;
using Shearlegs.Web.API.Services.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : RESTFulController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
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
    }
}
