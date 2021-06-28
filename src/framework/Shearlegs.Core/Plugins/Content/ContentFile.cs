using Shearlegs.API.Plugins.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins.Content
{
    public class ContentFile : IContentFile
    {
        public ContentFile(string name, Stream content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; set; }

        public Stream Content { get; set; }
    }
}
