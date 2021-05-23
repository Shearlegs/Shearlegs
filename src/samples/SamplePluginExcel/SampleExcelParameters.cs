using Shearlegs.API.Plugins.Attributes;

namespace SamplePluginExcel
{
    [Parameters]
    public class SampleExcelParameters
    {
        public string FileName { get; set; }
        public string WorksheetName { get; set; }
        [Secret]
        public string ConnectionString { get; set; }
    }
}
