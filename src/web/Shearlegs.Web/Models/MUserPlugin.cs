using System;
using System.Collections.Generic;

namespace Shearlegs.Web.Models
{
    public class MUserPlugin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PluginId { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }

        public MPlugin Plugin { get; set; }
    }
}
