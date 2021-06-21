using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shearlegs.Web.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public string Username => accessor.HttpContext.User.Identity.Name;
        public int UserId => int.Parse(accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public string Role => accessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        public bool IsAuthenticated => accessor.HttpContext.User.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(string role) => accessor.HttpContext.User.IsInRole(role);
    }
}
