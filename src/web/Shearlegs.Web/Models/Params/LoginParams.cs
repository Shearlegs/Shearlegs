using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Models.Params
{
    public class LoginParams
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
