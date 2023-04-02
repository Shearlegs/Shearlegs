using Shearlegs.Web.Shared.Enums;
using System.Text.Json.Serialization;

namespace Shearlegs.Web.API.Models.Users.Params
{
    public class ModifyUserIdentityParams
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public UserRole Role { get; set; }
        public string Password { get; set; }
    }
}
