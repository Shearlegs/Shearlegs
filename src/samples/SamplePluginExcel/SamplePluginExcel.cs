using OfficeOpenXml;
using SamplePluginExcel.Models;
using SamplePluginExcel.Services;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SamplePluginExcel
{
    public class SamplePluginExcel : Plugin
    {
        private readonly SampleExcelParameters parameters;
        private readonly DatabaseService database;
        private readonly ExcelService excel;

        public SamplePluginExcel(SampleExcelParameters parameters, DatabaseService database, ExcelService excel)
        {
            this.parameters = parameters;
            this.database = database;
            this.excel = excel;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public override async Task<IPluginResult> ExecuteAsync()
        {
            IEnumerable<InformationSchemaTable> data = await database.GetTablesAsync();

            string fileName = parameters.FileName;
            string mimeType = ExcelService.MimeType;
            byte[] fileData = await excel.BuildExcelFileAsync(data);

            return await File(fileName, mimeType, fileData);
        }
    }
}
