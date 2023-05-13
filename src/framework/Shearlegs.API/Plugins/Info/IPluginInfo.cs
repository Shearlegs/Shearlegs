using System.Collections.Generic;

namespace Shearlegs.API.Plugins.Info
{
    public interface IPluginInfo
    {
        string PackageId { get; }
        string Version { get; }
        bool IsPrerelease { get; }
        IEnumerable<IPluginParameterInfo> Parameters { get; }
        IEnumerable<IContentFileInfo> ContentFiles { get; }
    }
}
