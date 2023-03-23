using System.Text;
using System.Text.Json.Serialization;

namespace Shearlegs.Web.API.Models.Options
{
    public class JWTOptions
    {
        public const string SectionKey = "Jwt";

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }

        [JsonIgnore]
        public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key);
    }
}
