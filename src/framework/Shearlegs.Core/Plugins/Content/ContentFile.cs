using Shearlegs.API.Plugins.Content;
using System.IO;
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

        public Task<string> GetTextAsync()
        {
            return GetTextAsync(Encoding.UTF8);
        }

        public async Task<string> GetTextAsync(Encoding encoding)
        {
            using StreamReader reader = new(Content, encoding);
            return await reader.ReadToEndAsync();
        }

        public async Task<byte[]> GetBytesAsync()
        {
            using MemoryStream ms = new();
            await Content.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}
