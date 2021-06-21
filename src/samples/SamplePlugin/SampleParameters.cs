using Shearlegs.API.Plugins.Attributes;

namespace SamplePlugin
{
    [Parameters]
    public class SampleParameters
    {
        [Parameter(IsRequired = false, Description = "This is a text parameter description")]
        public string Text { get; set; } = "Hello World!";
        [Secret]
        public string Secret { get; set; }
        [Parameter(IsRequired = true, Description = "This is a cool number parameter description")]
        public int CoolNumber { get; set; }
    }
}
