using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ParameterAttribute : Attribute
    {
        public bool IsRequired { get; set; } = false;
        public string Description { get; set; }
    }
}
