using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Models
{
    public class MPluginSecret
    {
        public int Id { get; set; }
        public int PluginId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsArray { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
