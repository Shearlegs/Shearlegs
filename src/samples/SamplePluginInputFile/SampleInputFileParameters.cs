using Shearlegs.API.Plugins.Attributes;
using Shearlegs.API.Plugins.Parameters;
using Shearlegs.Core.Plugins;

namespace SamplePluginInputFile
{
    public class SampleInputFileParameters : Parameters
    {
        [Parameter(IsRequired = true, Description = "Import file")]
        public FileParameter ImportFile { get; set; }
        [Parameter(IsRequired = true, Description = "Output file name")]
        public string FileName { get; set; }
    }
}
