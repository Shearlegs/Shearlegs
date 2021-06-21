using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins.Content
{
    public interface IContentFileStore
    {
        IEnumerable<IContentFile> Files { get; }
        IContentFile GetFile(string name, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);
    }
}
