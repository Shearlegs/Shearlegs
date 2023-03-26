using Shearlegs.Web.APIClient.Models.UserAuthentications;
using Shearlegs.Web.APIClient.Models.Users;
using Shearlegs.Web.APIClient.Models.UserSessions;

namespace Shearlegs.Web.Dashboard.Services
{
    public class UserState
    {
        public bool IsAuthenticated => AuthenticatedUser != null;
        public AuthenticatedUser AuthenticatedUser { get; set; }
        public User User => AuthenticatedUser.User;
        public UserSession Session => AuthenticatedUser.UserSession;

        public void SetAuthenticatedUser(AuthenticatedUser authenticatedUser)
        {
            AuthenticatedUser = authenticatedUser;
        }
    }
}
