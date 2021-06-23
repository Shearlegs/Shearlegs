using Shearlegs.API.Plugins.Attributes;
using Shearlegs.Core.Plugins;

namespace SamplePlugin
{
    public class SampleParameters : Parameters
    {
        [Parameter(IsRequired = true, Description = "This parameter value will be print out in the result")]
        public string Text { get; set; } = "Hello World!";
    }
}
