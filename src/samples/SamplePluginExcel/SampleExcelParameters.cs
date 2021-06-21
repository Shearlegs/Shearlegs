using Shearlegs.API.Plugins.Attributes;

namespace SamplePluginExcel
{
    [Parameters]
    public class SampleExcelParameters
    {
        [Parameter(IsRequired = true, Description = "Excel document output file name")]
        public string FileName { get; set; }
        [Parameter(IsRequired = true, Description = "Worksheet name used in the excel document")]
        public string WorksheetName { get; set; }
        [Secret]
        public string ConnectionString { get; set; }
    }
}
