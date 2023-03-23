using Shearlegs.Web.APIClient.Models.Users;
using Shearlegs.Web.APIClient.Models.UserSessions;

namespace Shearlegs.Web.APIClient.Models.UserAuthentications
{
    public class AuthenticatedUser
    {
        public User User { get; set; }
        public UserSession UserSession { get; set; }
    }
}
