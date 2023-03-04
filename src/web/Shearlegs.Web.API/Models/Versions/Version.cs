using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.VersionParameters;
using System;
using System.Collections.Generic;

namespace Shearlegs.Web.API.Models.Versions
{
    public class Version
    {
        public int Id { get; set; }
        public int PluginId { get; set; }
        public string Name { get; set; }
        public int ContentLength { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo CreateUser { get; set; }
        public List<VersionParameter> Parameters { get; set; }
    }
}
