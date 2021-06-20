using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Core.Plugins.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins
{
    public abstract class PluginBase : IPluginBase
    {
        public PluginBase()
        {
            Assembly = GetType().Assembly;
            Name = GetType().Name;
            Version = Assembly.GetName().Version.ToString();
            Author = null;
        }

        public virtual string Name { get; }
        public virtual string Version { get; }
        public virtual string Author { get; }

        public Assembly Assembly { get; }

        protected Task<IPluginResult> Text(string text, bool isMarkupString = false) => Task.FromResult((IPluginResult)(new PluginTextResult(text, isMarkupString)));
        protected Task<IPluginResult> File(string name, string mimeType, byte[] content) => Task.FromResult((IPluginResult)(new PluginFileResult(name, mimeType, content)));
        protected Task<IPluginResult> Error(string message, Exception exception = null) => Task.FromResult((IPluginResult)(new PluginErrorResult(message, exception)));
    }
}
