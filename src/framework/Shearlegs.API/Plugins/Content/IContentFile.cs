using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins.Content
{
    public interface IContentFile
    {
        string Name { get; }
        byte[] Data { get; }
    }
}
