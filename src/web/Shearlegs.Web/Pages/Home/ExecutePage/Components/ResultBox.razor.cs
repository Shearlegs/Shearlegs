using Microsoft.AspNetCore.Components;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins.Result;

namespace Shearlegs.Web.Pages.Home.ExecutePage.Components
{
    public partial class ResultBox
    {
        [Parameter]
        public IPluginResult Result { get; set; }

        public PluginTextResult TextResult => Result as PluginTextResult;
        public PluginFileResult FileResult => Result as PluginFileResult;

    }
}
