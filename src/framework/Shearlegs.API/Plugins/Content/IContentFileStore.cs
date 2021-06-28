using System.Collections.Generic;

namespace Shearlegs.API.Plugins.Content
{
    public interface IContentFileStore
    {
        IEnumerable<IContentFile> Files { get; }
        IContentFile GetFile(string name);
    }
}
