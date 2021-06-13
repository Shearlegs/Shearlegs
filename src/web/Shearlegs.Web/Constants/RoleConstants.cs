using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Constants
{
    public class RoleConstants
    {
        public static readonly string[] Roles = new string[]
        {
            AdminRoleId,
            DeveloperRoleId,
            GuestRoleId
        };

        public const string AdminRoleId = "Admin";
        public const string DeveloperRoleId = "Developer";
        public const string GuestRoleId = "Guest";
    }
}
