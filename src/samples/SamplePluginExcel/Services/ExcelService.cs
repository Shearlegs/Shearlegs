using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using SamplePluginExcel.Models;
using Shearlegs.API.Plugins.Attributes;
using Shearlegs.API.Plugins.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePluginExcel.Services
{
    [Service(Lifetime = ServiceLifetime.Transient)]
    public class ExcelService
    {
        public const string Extension = ".xlsx";
        public const string MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        private readonly SampleExcelParameters parameters;
        private readonly IContentFileStore fileStore;

        public ExcelService(SampleExcelParameters parameters, IContentFileStore fileStore)
        {
            this.parameters = parameters;
            this.fileStore = fileStore;
        }

        public async Task<byte[]> BuildExcelFileAsync(IEnumerable<InformationSchemaTable> data)
        {
            IContentFile file = fileStore.GetFile("template.xlsx");
            byte[] result;
            using ExcelPackage pckg = new(file.Content);
            ExcelWorksheet worksheet = pckg.Workbook.Worksheets.FirstOrDefault();
            worksheet.Name = parameters.WorksheetName;

            worksheet.Cells.LoadFromCollection(data, true);
            worksheet.Cells.AutoFitColumns();

            result = await pckg.GetAsByteArrayAsync();

            return result;
        }
    }
}
