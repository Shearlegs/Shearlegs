using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.Users.Params
{
    public class AddUserParams
    {
        public string Name { get; set; }
        public UserRole Role { get; set; }
        public UserAuthenticationType AuthenticationType { get; set; }
        public string PasswordHash { get; set; }
    }
}
