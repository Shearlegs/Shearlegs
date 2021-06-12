using Shearlegs.API.Plugins.Attributes;

namespace SamplePlugin
{
    [Parameters]
    public class SampleParameters
    {
        [Parameter(IsRequired = false, Description = "This is a text parameter description")]
        public string Text { get; set; } = "Hello World!";
        [Secret(IsRequired = true, Description = "This is secret parameter description")]
        public string Secret { get; set; }
    }
}
