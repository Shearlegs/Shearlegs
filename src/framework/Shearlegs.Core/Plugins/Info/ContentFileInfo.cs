using Shearlegs.API.Plugins.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins.Info
{
    public class ContentFileInfo : IContentFileInfo
    {
        public ContentFileInfo(string name, long length)
        {
            Name = name;
            Length = length;
        }

        public string Name { get; set; }

        public long Length { get; set; }
    }
}
