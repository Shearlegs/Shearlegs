using Shearlegs.API.Plugins.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins
{
    public interface IPlugin : IPluginBase
    {
        Task<IPluginResult> ExecuteAsync();
    }
}
