using System;
using System.Text.Json.Serialization;

namespace Shearlegs.Web.API.Models.UserSessions
{
    public class UserSession
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string AuthenticationMethod { get; set; }
        public string AuthenticationScheme { get; set; }
        public string HostName { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTimeOffset ExpireDate { get; set; }
        public DateTimeOffset? RevokeDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        [JsonIgnore]
        public bool IsInvalid => ExpireDate < DateTimeOffset.Now || RevokeDate.HasValue;
    }
}
