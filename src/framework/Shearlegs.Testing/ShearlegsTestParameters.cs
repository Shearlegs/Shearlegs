using Shearlegs.API.Plugins.Parameters;
using System.IO;

namespace Shearlegs.Testing
{
    public class ShearlegsTestParameters
    {
        /// <summary>
        /// Creates a FileParameter from the file in the specified path
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>new FileParameter instance</returns>
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
