using Shearlegs.API.Exceptions;
using Shearlegs.API.Plugins.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.Plugins.Content
{
    public class ContentFileStore : IContentFileStore
    {
        private readonly IEnumerable<IContentFile> files;

        public ContentFileStore(IEnumerable<IContentFile> files)
        {
            this.files = files;
        }

        public IEnumerable<IContentFile> Files => files;

        public IContentFile GetFile(string name, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            IContentFile file = files.FirstOrDefault(x => x.Name.Equals(name, stringComparison));
            if (file == null)
            {
                throw new ContentFileNotFoundException(name);
            }

            return file;
        }
    }
}
