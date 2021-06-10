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
            GuestRoleId
        };

        public const string AdminRoleId = "Admin";
        public const string GuestRoleId = "Guest";
    }
}
