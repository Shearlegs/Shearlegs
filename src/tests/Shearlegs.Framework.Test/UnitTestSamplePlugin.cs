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
    public class UnitTestSamplePlugin
    {
        const string PluginPath = @"C:\Users\Michal\projects\Github\Shearlegs\Shearlegs\src\samples\SamplePlugin\bin\Debug\SamplePlugin.1.0.0.nupkg";

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
            PluginTextResult textResult = result as PluginTextResult;

            Console.WriteLine(textResult.Text);
        }   
    }
}
