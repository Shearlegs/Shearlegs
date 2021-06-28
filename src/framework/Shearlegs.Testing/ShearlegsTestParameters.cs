using Shearlegs.API.Plugins.Parameters;
using System.IO;

namespace Shearlegs.Testing
{
    public class ShearlegsTestParameters
    {
        public FileParameter FileParameter(string path)
        {
            return new FileParameter()
            {
                Content = File.ReadAllBytes(path),
                Name = Path.GetFileName(path)
            };
        }
    }
}
