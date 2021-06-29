using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Plugins.Content
{
    public interface IContentFile
    {
        string Name { get; }
        Stream Content { get; }

        Task<byte[]> GetBytesAsync();
        Task<string> GetTextAsync();
        Task<string> GetTextAsync(Encoding encoding);
    }
}
