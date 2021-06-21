using Shearlegs.API.Plugins.Attributes;

namespace SamplePluginExcel
{
    [Parameters]
    public class SampleExcelParameters
    {
        [Parameter(IsRequired = true)]
        public string FileName { get; set; }
        [Parameter(IsRequired = true)]
        public string WorksheetName { get; set; }
        [Secret]
        public string ConnectionString { get; set; }
    }
}
