using Microsoft.AspNetCore.Components;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Home.ResultPage.Components
{
    public partial class FileResult
    {
        [Parameter]
        public MResult Result { get; set; }

        public PluginFileResult PluginFileResult { get; set; }

        protected override void OnParametersSet()
        {
            PluginFileResult = Result.Deserialize<PluginFileResult>();
        }
    }
}
