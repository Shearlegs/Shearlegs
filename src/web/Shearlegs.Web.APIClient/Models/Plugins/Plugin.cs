using Shearlegs.Web.APIClient.Models.Users;
using System;

namespace Shearlegs.Web.APIClient.Models.Plugins
{
    public class Plugin
    {
        public int Id { get; set; }
        public string PackageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo UpdateUser { get; set; }
        public UserInfo CreateUser { get; set; }
    }
}
