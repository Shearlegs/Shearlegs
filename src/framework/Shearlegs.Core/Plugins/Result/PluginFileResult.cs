using Shearlegs.API.Plugins.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins.Result
{
    public class PluginFileResult : IPluginResult
    {
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }

        public PluginFileResult(string name, string mimeType, byte[] content)
        {
            Name = name;
            MimeType = mimeType;
            Content = content;
        }
    }
}
