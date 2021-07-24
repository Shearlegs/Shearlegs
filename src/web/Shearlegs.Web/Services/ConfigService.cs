using Microsoft.Extensions.Configuration;
using Shearlegs.Web.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Shearlegs.Web.Services
{
    public class ConfigService
    {
        private readonly IConfiguration configuration;

        public ConfigService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool IsDemo => configuration.GetValue<bool>("IsDemo");
        public bool IsWindowsAuthEnabled => configuration.GetSection("AuthenticationTypes").GetValue<bool>("Windows");
        public bool IsDefaultAuthEnabled => configuration.GetSection("AuthenticationTypes").GetValue<bool>("Default");

        public IEnumerable<string> EnabledAuthenticationTypes => AuthenticationConstants.AuthenticationTypes.Where(x => IsAuthEnabled(x));
        public bool IsAuthEnabled(string authType) => configuration.GetSection("AuthenticationTypes").GetValue<bool>(authType);

        public string IconPath => IsDemo ? "/img/icon_demo.png" : "/img/icon.png";
    }
}
