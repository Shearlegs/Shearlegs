using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins.Parameters
{
    public class PluginParameterInfo
    {   
        public string Name { get; set; }
        public string Description { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; }
        public bool IsRequired { get; set; }
        public bool IsSecret { get; set; }
    }
}
