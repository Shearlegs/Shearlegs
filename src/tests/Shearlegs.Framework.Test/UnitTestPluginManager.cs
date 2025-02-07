using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Runtime;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Shearlegs.Framework.Test
{
    [TestClass]
    public class UnitTestPluginManager
    {
        const string PluginPath = @"C:\Users\Michal\projects\Github\Shearlegs\Shearlegs\src\samples\SamplePlugin\bin\Debug\SamplePlugin.1.0.9.nupkg";

        JObject parameters = JObject.FromObject(new 
        { 
            Text = "Hello world!",
            Secret = "IAmSecret"
        });

        [TestMethod]
        public async Task TestSamplePlugin()
        {
            IServiceProvider serviceProvider = ShearlegsRuntime.BuildServiceProvider();

            IPluginManager pluginManager = serviceProvider.GetRequiredService<IPluginManager>();

            byte[] pluginData = await File.ReadAllBytesAsync(PluginPath);
            string json = parameters.ToString();

            IPluginResult result = await pluginManager.ExecutePluginAsync(pluginData, json);
            Assert.IsTrue(result is PluginTextResult);
        }   

        [TestMethod]
        public async Task TestGetParameters()
        {
            IServiceProvider serviceProvider = ShearlegsRuntime.BuildServiceProvider();

            IPluginManager pluginManager = serviceProvider.GetRequiredService<IPluginManager>();

            byte[] pluginData = await File.ReadAllBytesAsync(PluginPath);

            IPluginInfo info = await pluginManager.GetPluginInfoAsync(pluginData);
            Assert.IsTrue(info is not null);
        }

        [TestMethod]
        public async Task TestSamplePluginExcel()
        {
            const string PluginPath = @"C:\Users\Michal\projects\Github\Shearlegs\Shearlegs\src\samples\SamplePluginExcel\bin\Debug\SamplePluginExcel.1.0.10.nupkg";
            JObject parameters = JObject.FromObject(new
            {
                FileName = "test.xlsx",
                WorksheetName = "INFORMATION_SCHEMA.TABLES",
                ConnectionString = "Server=localhost;Database=Shearlegs;Trusted_Connection=True;"
            });

            IServiceProvider serviceProvider = ShearlegsRuntime.BuildServiceProvider();

            IPluginManager pluginManager = serviceProvider.GetRequiredService<IPluginManager>();

            await DoTest(pluginManager);
            await DoTest(pluginManager);

            async Task DoTest(IPluginManager pluginManager)
            {
                byte[] pluginData = await File.ReadAllBytesAsync(PluginPath);
                string json = parameters.ToString();

                IPluginResult result = await pluginManager.ExecutePluginAsync(pluginData, json);

                Assert.IsTrue(result is PluginFileResult);
            }
        }
    }
}
