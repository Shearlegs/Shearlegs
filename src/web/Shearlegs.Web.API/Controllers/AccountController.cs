using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Services.Processings.Users;
using System;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : RESTFulController
    {
        private readonly IUserProcessingService userProcessingService;

        public AccountController(IUserProcessingService userProcessingService)
        {
            this.userProcessingService = userProcessingService;
        }

        [HttpGet("user")]
        public async ValueTask<IActionResult> GetAccountUser()
        {
            string username = HttpContext.Request.Headers["Username"];
            Console.WriteLine($"username: {username}");

            User user = await userProcessingService.RetrieveUserByNameAsync(username);

            return Ok(user);
        }
    }
}
