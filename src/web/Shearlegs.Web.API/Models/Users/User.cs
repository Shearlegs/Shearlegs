using Shearlegs.Web.Shared.Enums;
using System;
using System.Text.Json.Serialization;

namespace Shearlegs.Web.API.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
        public UserAuthenticationType AuthenticationType { get; set; }
        public DateTimeOffset LastLoginDate  { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
