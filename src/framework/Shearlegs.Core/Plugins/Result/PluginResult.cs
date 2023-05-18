using Shearlegs.API.Plugins.Result;

namespace Shearlegs.Core.Plugins.Result
{
    public class PluginResult : IPluginResult
    {
        public virtual string ResultType { get; }
    }
}
