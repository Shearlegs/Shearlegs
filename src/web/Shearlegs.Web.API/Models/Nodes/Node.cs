using Shearlegs.Web.API.Models.Users;
using System;

namespace Shearlegs.Web.API.Models.Nodes
{
    public class Node
    {
		public int Id { get; set; }
		public Guid UUID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string FQDN { get; set; }
		public string Scheme { get; set; }
		public bool IsBehindProxy { get; set; }
		public bool IsEnabled { get; set; }
		public Guid AccessToken { get; set; }        
		public DateTimeOffset? UpdateDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo UpdateUser { get; set; }
        public UserInfo CreateUser { get; set; }
    }
}
