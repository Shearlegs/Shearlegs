using System;

namespace Shearlegs.Web.API.Models.UserSessions.Params
{
    public class GetUserSessionsParams
    {
        public Guid SessionId { get; set; }
        public int UserId { get; set; }
    }
}
