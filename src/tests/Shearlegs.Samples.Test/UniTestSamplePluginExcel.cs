using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Runtime;
using Shearlegs.Testing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Shearlegs.Samples.Test
{
    [TestClass]
    public class UniTestSamplePluginExcel
    {
        [TestMethod]
        public async Task ExecutePlugin()
        {
            object parameters = new
            {
                FileName = "test.xlsx",
                WorksheetName = "INFORMATION_SCHEMA.TABLES",
                ConnectionString = "Server=localhost;Database=Shearlegs;Trusted_Connection=True;"
            };

            PluginFileResult result = await ShearlegsTest.ExecutePluginAsync<SamplePluginExcel.SamplePluginExcel, PluginFileResult>(parameters);
            Console.WriteLine(ShearlegsTest.Results.Save(result));
        }
    }
}
