namespace Shearlegs.Web.API.Models.UserSessions.Params
{
    public class CreateUserSessionParams
    {
        public int UserId { get; set; }
        public string HostName { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
    }
}
