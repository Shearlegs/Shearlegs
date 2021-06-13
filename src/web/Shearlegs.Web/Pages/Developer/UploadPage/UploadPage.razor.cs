using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Info;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Developer.UploadPage
{
    public partial class UploadPage
    {
        [Inject]
        public IPluginManager PluginManager { get; set; }

        private byte[] pluginData;
        public IPluginInfo PluginInfo { get; set; }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            if (e.FileCount == 0)
                return;

            pluginData = new byte[e.File.Size];
            await e.File.OpenReadStream(30 * 1024 * 1024).ReadAsync(pluginData);

            PluginInfo = await PluginManager.GetPluginInfoAsync(pluginData);                        
        }
    }
}
