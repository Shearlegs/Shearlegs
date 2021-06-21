using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins.Info
{
    public interface IContentFileInfo
    {
        string Name { get; }
        int Length { get; }
    }
}
