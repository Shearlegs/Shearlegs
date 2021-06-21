using Shearlegs.API.Plugins.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins.Info
{
    public class PluginInfo : IPluginInfo
    {
        public string PackageId { get; set; }
        public string Version { get; set; }
        public bool IsPrerelease { get; set; }

        public IEnumerable<IPluginParameterInfo> Parameters { get; set; }
        public IEnumerable<IContentFileInfo> ContentFiles { get; set; }
    }
}
