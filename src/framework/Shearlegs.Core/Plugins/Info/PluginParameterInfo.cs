using Shearlegs.API.Plugins.Info;
using System;

namespace Shearlegs.Core.Plugins.Info
{
    public class PluginParameterInfo : IPluginParameterInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; }
        public bool IsRequired { get; set; }
        public bool IsSecret { get; set; }
    }
}
