using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.Shared.Enums;
using System;

namespace Shearlegs.Web.API.Models.Results
{
    public class Result
    {
        public int Id { get; set; }
        public ResultStatus Status { get; set; }
        public string ResultType { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo User { get; set; }
        public VersionInfo Version { get; set; }
        public PluginInfo Plugin { get; set; }        
    }
}
