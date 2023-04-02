using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.APIClient.Models.Users.Requests
{
    public class ModifyUserIdentityRequest
    {
        public UserRole Role { get; set; }
        public string Password { get; set; }
    }
}
