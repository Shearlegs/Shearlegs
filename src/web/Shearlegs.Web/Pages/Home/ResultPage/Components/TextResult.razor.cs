using Microsoft.AspNetCore.Components;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Web.Models;

namespace Shearlegs.Web.Pages.Home.ResultPage.Components
{
    public partial class TextResult
    {
        [Parameter]
        public MResult Result { get; set; }

        public PluginTextResult PluginTextResult { get; set; }

        protected override void OnParametersSet()
        {
            PluginTextResult = Result.Deserialize<PluginTextResult>();
        }
    }
}
