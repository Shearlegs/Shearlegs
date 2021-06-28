using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamplePluginInputFile;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Test;
using System.IO;
using System.Threading.Tasks;

namespace Shearlegs.Samples.Test
{
    [TestClass]
    public class UniTestSamplePluginInputFile
    {
        [TestMethod]
        public async Task ExecutePlugin()
        {
            object parameters = new
            {
                FileName = "editedFile.txt",
                ImportFile = ShearlegsTest.Parameters.FileParameter("importFile.txt")
            };

            PluginFileResult result = await ShearlegsTest.ExecutePluginAsync<SamplePluginInputFile.SamplePluginInputFile, PluginFileResult>(parameters);
            System.Console.WriteLine(ShearlegsTest.Results.Save(result));
        }
    }
}
