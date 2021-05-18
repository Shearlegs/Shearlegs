using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Runtime;
using System.IO;
using System.Threading.Tasks;

namespace Shearlegs.Framework.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var servicveProvider = ShearlegsRuntime.BuildServiceProvider();

            var pluginManager = servicveProvider.GetRequiredService<IPluginManager>();

            var pluginData = 
                await File.ReadAllBytesAsync(@"C:\Users\Michal\projects\Github\Shearlegs\Shearlegs\src\samples\SamplePlugin\bin\Debug\SamplePlugin.1.0.0.nupkg");

            string json =
                await File.ReadAllTextAsync(@"C:\Users\Michal\projects\Github\Shearlegs\Shearlegs\src\samples\SamplePlugin\bin\Debug\parameters.json");

            IPluginResult result = await pluginManager.ExecutePluginAsync(pluginData, json);
            PluginTextResult textResult = result as PluginTextResult;

            System.Console.WriteLine(textResult.Text);

        }
    }
}
