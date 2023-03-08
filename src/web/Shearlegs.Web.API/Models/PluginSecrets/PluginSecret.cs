using Shearlegs.Web.API.Models.Users;
using System;

namespace Shearlegs.Web.API.Models.PluginSecrets
{
    public class PluginSecret
    {
        public int Id { get; set; }
        public int PluginId { get; set; }
        public string Name { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo UpdateUser { get; set; }
        public UserInfo CreateUser { get; set; }        
    }
}
