using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Result;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins
{
    public abstract class Plugin : PluginBase, IPlugin
    {
        public virtual Task<IPluginResult> ExecuteAsync()
        {
            return Text("Result Unspecified");
        }
    }
}
