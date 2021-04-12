using System.Reflection;

namespace Shearlegs.API.Plugins
{
    public interface IPluginBase
    {
        string Name { get; }
        string Version { get; }
        string Author { get; }
        Assembly Assembly { get; }
    }
}
