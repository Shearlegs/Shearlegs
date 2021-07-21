using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shearlegs.Web.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Shearlegs.Web.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IConfiguration configuration;

        public UserService(IHttpContextAccessor accessor, IConfiguration configuration)
        {
            this.accessor = accessor;
            this.configuration = configuration;            
        }

        public string Username => accessor.HttpContext.User.Identity.Name;
        public int UserId
        {
            get
            {
                if (!IsAuthenticated)
                    return 0;

                int.TryParse(accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
                return userId;
            }
        }

        public string Role => accessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        public bool IsAuthenticated => accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public bool IsWindowsAuthType => configuration.GetValue<string>("AuthenticationType") == "Windows";
        public bool IsDefaultAuthType => configuration.GetValue<string>("AuthenticationType") == "Default";

        public bool HasUserId => UserId != 0;

        public bool IsInRole(string role) => accessor.HttpContext.User.IsInRole(role);
    }
}
