﻿using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using SamplePluginExcel.Models;
using Shearlegs.API.Plugins.Attributes;
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

        public ExcelService(SampleExcelParameters parameters)
        {
            this.parameters = parameters;
        }

        public async Task<byte[]> BuildExcelFileAsync(IEnumerable<InformationSchemaTable> data)
        {
            
            FileInfo templateFile = new FileInfo("template.xlsx");
            byte[] result;
            using ExcelPackage pckg = new(templateFile);
            ExcelWorksheet worksheet = pckg.Workbook.Worksheets.Add(parameters.WorksheetName);

            worksheet.Cells.LoadFromCollection(data, true);

            worksheet.Cells[6, 6].Value = Directory.GetCurrentDirectory();

            result = await pckg.GetAsByteArrayAsync();

            return result;
        }
    }
}
