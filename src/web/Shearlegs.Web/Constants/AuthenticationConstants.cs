using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Constants
{
    public class AuthenticationConstants
    {
        public static readonly string[] WindowsAuthTypes = new string[]
        {
            "NTLM",
            "Negotiate"
        };

        public const string WindowsAuthenticationType = "Windows";
        public const string DefaultAuthenticationType = "Default";

        public static readonly string[] AuthenticationTypes = new string[]
        {
            DefaultAuthenticationType,
            WindowsAuthenticationType
        };
    }
}
