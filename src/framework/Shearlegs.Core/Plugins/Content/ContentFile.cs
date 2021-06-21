using Shearlegs.API.Plugins.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins.Content
{
    public class ContentFile : IContentFile
    {
        public ContentFile(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }

        public string Name { get; set; }

        public byte[] Data { get; set; }
    }
}
