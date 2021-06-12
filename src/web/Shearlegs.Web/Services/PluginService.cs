using Shearlegs.API.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Services
{
    public class PluginService
    {
        private readonly IPluginManager pluginManager;

        public PluginService(IPluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
        }


    }
}
