using System;

namespace Shearlegs.Web.API.Models.Nodes
{
    public class NodeInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FQDN { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
