using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Models.UserSessions
{
    public class UserSession
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string AuthenticationMethod { get; set; }
        public string AuthenticationScheme { get; set; }
        public string HostName { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTimeOffset ExpireDate { get; set; }
        public DateTimeOffset? RevokeDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
