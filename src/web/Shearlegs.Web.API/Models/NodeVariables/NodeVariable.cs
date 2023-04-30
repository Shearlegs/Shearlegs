using Shearlegs.Web.API.Models.Users;
using System;

namespace Shearlegs.Web.API.Models.NodeVariables
{
    public class NodeVariable
    {
		public int Id { get; set; }
		public int NodeId { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
		public bool IsSensitive { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo UpdateUser { get; set; }
        public UserInfo CreateUser { get; set; }
    }
}
