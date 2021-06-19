using Shearlegs.API.Plugins.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins.Result
{
    public class PluginTextResult : PluginResult
    {        
        public string Text { get; set; }
        public bool IsMarkupString { get; set; }

        public PluginTextResult(string text, bool isMarkupString)
        {
            Text = text;
            IsMarkupString = isMarkupString;
        }
    }
}
