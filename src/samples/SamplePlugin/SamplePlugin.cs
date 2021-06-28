using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins;
using System.Threading.Tasks;

namespace SamplePlugin
{
    public class SamplePlugin : Plugin
    {
        private readonly SampleParameters parameters;

        public SamplePlugin(SampleParameters parameters)
        {
            this.parameters = parameters;
        }

        public override Task<IPluginResult> ExecuteAsync()
        {
            string textResult = $"Your Text parameter input: {parameters.Text}"; 

            return Task.FromResult(Text(textResult));
        }
    }
}
