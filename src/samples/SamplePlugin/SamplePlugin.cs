using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins;
using System.Text;
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
            string str = string.Join("<br>",
                "This is start of sample plugin result!",
                $"Text from plugin parameters: {parameters.Text}",
                $"Cool Number from plugin parameters: {parameters.CoolNumber}",
                $"Secret from plugin parameters: {parameters.Secret}",
                "This is end of sample plugin result!");

            return Text(str);
        }
    }
}
