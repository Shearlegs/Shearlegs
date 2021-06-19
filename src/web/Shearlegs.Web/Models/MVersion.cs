using System;
using System.Collections.Generic;

namespace Shearlegs.Web.Models
{
    public class MVersion
    {
        public int Id { get; set; }
        public int PluginId { get; set; }
        public string Name { get; set; }
        public byte[] PackageContent { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }

        public List<MVersionParameter> Parameters { get; set; }

        public MPlugin Plugin { get; set; }
    }
}
