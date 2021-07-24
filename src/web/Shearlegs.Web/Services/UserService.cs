using Microsoft.AspNetCore.Http;
using Shearlegs.Web.Constants;
using System;
using System.Linq;
using System.Security.Claims;

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
        public int UserId
        {
            get
            {
                if (!IsCookieAuthType)
                    return 0;

                int.TryParse(accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
                return userId;
            }
        }
        public string Role => accessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        public bool IsAuthenticated => accessor.HttpContext.User.Identity?.IsAuthenticated ?? false;
        public bool IsWindowsAuthType => IsAuthenticated && AuthenticationConstants.WindowsAuthTypes.Contains(accessor.HttpContext.User.Identity?.AuthenticationType ?? null);
        public bool IsCookieAuthType => IsAuthenticated && (accessor.HttpContext.User.Identity?.AuthenticationType ?? null) == "Cookies";
        public bool IsInRole(string role) => IsAuthenticated && accessor.HttpContext.User.IsInRole(role);
    }
}
