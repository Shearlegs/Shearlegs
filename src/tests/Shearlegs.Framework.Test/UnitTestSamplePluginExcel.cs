using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Runtime;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Shearlegs.Framework.Test
{
    [TestClass]
    public class UnitTestSamplePluginExcel
    {
        const string PluginPath = @"C:\Users\Michal\projects\Github\Shearlegs\Shearlegs\src\samples\SamplePluginExcel\bin\Debug\SamplePluginExcel.1.0.0.nupkg";
        const string OutputDirectory = @"C:\Users\Michal\projects\Github\Shearlegs\Shearlegs\src\samples\SamplePluginExcel\bin\Debug\";

        JObject parameters = JObject.FromObject(new
        {
            FileName = "test.xlsx",
            WorksheetName = "INFORMATION_SCHEMA.TABLES",
            ConnectionString = "Server=localhost;Database=Shearlegs;Trusted_Connection=True;"
        });

        [TestMethod]
        public async Task TestSamplePluginExcel()
        {
            IServiceProvider serviceProvider = ShearlegsRuntime.BuildServiceProvider();

            IPluginManager pluginManager = serviceProvider.GetRequiredService<IPluginManager>();

            byte[] pluginData = await File.ReadAllBytesAsync(PluginPath);
            string json = parameters.ToString();

            IPluginResult result = await pluginManager.ExecutePluginAsync(pluginData, json);
            PluginFileResult fileResult = result as PluginFileResult;
            
            string outputPath = Path.Combine(OutputDirectory, fileResult.Name);
            await File.WriteAllBytesAsync(outputPath, fileResult.Content);
        }
    }
}
