using Shearlegs.API.Plugins.Attributes;
using Shearlegs.Core.Plugins;

namespace SamplePluginExcel
{
    public class SampleExcelParameters : Parameters
    {
        [Parameter(IsRequired = true, Description = "Excel document output file name")]
        public string FileName { get; set; }
        [Parameter(IsRequired = true, Description = "Worksheet name used in the excel document")]
        public string WorksheetName { get; set; }
        [Secret]
        public string ConnectionString { get; set; }
    }
}
