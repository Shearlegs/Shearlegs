using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shearlegs.Web.Constants;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Models.Params;
using Shearlegs.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shearlegs.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UsersRepository usersRepository;
        private readonly UserService userService;

        public AuthenticationController(UsersRepository usersRepository, UserService userService)
        {
            this.usersRepository = usersRepository;
            this.userService = userService;
        }

        private async Task SignInUserAsync(MUser user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
            };

            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

            AuthenticationProperties authProperties = new()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7),
                IsPersistent = true,
                IssuedUtc = DateTime.UtcNow,
                RedirectUri = "/"
            };

            await usersRepository.UpdateLastLoginDateAsync(user.Id);
            await HttpContext.SignInAsync(claimsPrincipal, authProperties);
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        [HttpPost("~/windowsauth")]
        public async Task<IActionResult> WindowsAuthAsync()
        {
            if (!userService.IsWindowsAuthType)
                return NoContent();

            MUser user = await usersRepository.GetUserAsync(userService.Username);
            if (user == null)
            {
                user = await usersRepository.AddUserAsync(new MUser() 
                { 
                    Name = userService.Username,
                    Role = RoleConstants.GuestRoleId
                });
            }

            await SignInUserAsync(user);
            await usersRepository.UpdateLastLoginDateAsync(user.Id);
            
            return Redirect("/");
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        [HttpPost("~/signin")]
        public async Task<IActionResult> SignInAsync([FromForm] string username, [FromForm] string password)
        {
            MUser user = await usersRepository.GetUserAsync(username, password);

            if (user == null)
            {
                return Redirect("/login?isWrong=true");
            }

            await SignInUserAsync(user);
            return Redirect("/");
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        [HttpGet("~/signout"), HttpPost("~/signout")]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties() { RedirectUri = "/" });
            return Redirect("/");
        }
    }
}
