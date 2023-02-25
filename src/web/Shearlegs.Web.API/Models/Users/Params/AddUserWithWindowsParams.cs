using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.Users.Params
{
    public class AddUserWithWindowsParams
    {
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}
