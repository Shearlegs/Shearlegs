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
            StringBuilder sb = new StringBuilder();

            System.Console.WriteLine(Dapper.CommandFlags.Buffered.ToString()); 

            sb.AppendLine("This is start of sample plugin result!");
            sb.AppendLine("Text from plugin parameters: " + parameters.Text);
            sb.AppendLine("Secret from plugin parameters: " + parameters.Secret);
            sb.AppendLine("This is end of sample plugin result!");

            return Text(sb.ToString());
        }
    }
}
