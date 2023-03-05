using Shearlegs.Web.API.Models.Plugins;
using System;

namespace Shearlegs.Web.API.Models.Versions
{
    public class VersionInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
