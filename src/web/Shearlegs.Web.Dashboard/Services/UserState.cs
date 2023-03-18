using Shearlegs.Web.APIClient.Models.Users;

namespace Shearlegs.Web.Dashboard.Services
{
    public class UserState
    {
        public bool IsAuthenticated => User != null;
        public User User { get; set; }

        public void SetUser(User user)
        {
            User = user;
        }
    }
}
