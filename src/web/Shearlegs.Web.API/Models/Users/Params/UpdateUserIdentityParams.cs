using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.Users.Params
{
    public class UpdateUserIdentityParams
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
        public string PasswordHash { get; set; }
    }
}
