using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins;
using System.Threading.Tasks;

namespace SamplePluginInputFile
{
    public class SamplePluginInputFile : Plugin
    {
        private readonly SampleInputFileParameters parameters;

        public SamplePluginInputFile(SampleInputFileParameters parameters)
        {
            this.parameters = parameters;
        }

        public override Task<IPluginResult> ExecuteAsync()
        {
            IPluginResult result = File(parameters.FileName, parameters.ImportFile.MimeType, parameters.ImportFile.Content);
            return Task.FromResult(result);
        }
    }
}
