using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public string IconPath => IsDemo ? "/img/icon_demo.png" : "/img/icon.png";
    }
}
