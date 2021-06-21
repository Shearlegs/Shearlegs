using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
