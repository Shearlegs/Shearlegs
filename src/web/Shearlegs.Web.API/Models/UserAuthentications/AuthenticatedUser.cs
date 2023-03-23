using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.UserSessions;

namespace Shearlegs.Web.API.Models.UserAuthentications
{
    public class AuthenticatedUser
    {
        public User User { get; set; }
        public UserSession UserSession { get; set; }
    }
}
