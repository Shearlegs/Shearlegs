using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Runtime;
using Shearlegs.Testing;
using System.IO;
using System.Threading.Tasks;

namespace Shearlegs.Samples.Test
{
    [TestClass]
    public class UniTestSamplePlugin
    {
        [TestMethod]
        public async Task ExecutePlugin()
        {
            object parameters = new
            {
                Text = "Welcome Folks!"
            };

            PluginTextResult textResult = await ShearlegsTest.ExecutePluginAsync<SamplePlugin.SamplePlugin, PluginTextResult>(parameters);
            ShearlegsTest.Results.Print(textResult);
        }
    }
}
