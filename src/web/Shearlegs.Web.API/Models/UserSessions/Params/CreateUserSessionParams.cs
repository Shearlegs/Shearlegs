namespace Shearlegs.Web.API.Models.UserSessions.Params
{
    public class CreateUserSessionParams
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string HostName { get; set; }
    }
}
