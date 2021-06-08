using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Models.Params;
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

        public AuthenticationController(UsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        [HttpPost("~/signin")]
        public async Task<IActionResult> SignInAsync([FromBody] LoginParams loginParams)
        {
            User user = await usersRepository.GetUserAsync(loginParams.Name, loginParams.Password);

            if (user == null)
            {
                return BadRequest();
            }

            if (loginParams.ReturnUrl == null)
            {
                loginParams.ReturnUrl = "/";
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
            };

            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

            AuthenticationProperties authProperties = new()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddHours(24),
                IsPersistent = true,
                IssuedUtc = DateTime.UtcNow,
                RedirectUri = loginParams.ReturnUrl
            };

            return SignIn(claimsPrincipal, authProperties);
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        [HttpGet("~/signout"), HttpPost("~/signout")]
        public IActionResult SignOutAsync()
        {
            return SignOut(new AuthenticationProperties() { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
