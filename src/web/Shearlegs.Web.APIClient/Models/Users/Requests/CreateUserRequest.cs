using Shearlegs.Web.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Models.Users.Requests
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public UserRole Role { get; set; }
        public UserAuthenticationType AuthenticationType { get; set; }
        public string PasswordText { get; set; }
    }
}
