using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Parameters;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Runtime;
using System;
using System.Collections.Generic;
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

        [TestMethod]
        public async Task TestGetParameters()
        {
            IServiceProvider serviceProvider = ShearlegsRuntime.BuildServiceProvider();

            IPluginManager pluginManager = serviceProvider.GetRequiredService<IPluginManager>();

            byte[] pluginData = await File.ReadAllBytesAsync(PluginPath);

            IPluginInfo info = await pluginManager.GetPluginInfoAsync(pluginData);

            Console.WriteLine($"PackageId: {info.PackageId}");
            Console.WriteLine($"Version: {info.Version} PreRelease: {info.IsPrerelease}");
            foreach (IPluginParameterInfo parameter in info.Parameters)
            {
                Console.WriteLine($"Name: {parameter.Name} - Description: {parameter.Description} - Value: {parameter.Value}");
            }
        }
    }
}
