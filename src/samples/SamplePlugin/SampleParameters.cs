using Shearlegs.API.Plugins.Attributes;

namespace SamplePlugin
{
    [Parameters]
    public class SampleParameters
    {
        public string Text { get; set; }
        [Secret]
        public string Secret { get; set; }
    }
}
