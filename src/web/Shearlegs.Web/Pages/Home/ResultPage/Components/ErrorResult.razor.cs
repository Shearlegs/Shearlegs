using Microsoft.AspNetCore.Components;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Home.ResultPage.Components
{
    public partial class ErrorResult
    {
        [Parameter]
        public MResult Result { get; set; }

        public PluginErrorResult PluginErrorResult { get; set; }

        protected override void OnParametersSet()
        {
            PluginErrorResult = Result.Deserialize<PluginErrorResult>();
        }

        private bool isShow = false;
        public void ShowException()
        {
            isShow = true;
        }
    }
}
