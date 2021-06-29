using Shearlegs.Core.Plugins.Result;
using System;
using System.IO;

namespace Shearlegs.Testing
{
    public class ShearlegsTestResults
    {
        /// <summary>
        /// Saves the file on the disk in the current directory
        /// </summary>
        /// <param name="result">Plugin file result</param>
        /// <returns>File save path</returns>
        public string Save(PluginFileResult result)
        {
            return Save(result, Directory.GetCurrentDirectory());
        }

        /// <summary>
        /// Saves the file on the disk in the specified output directory
        /// </summary>
        /// <param name="result">Plugin file result</param>
        /// <param name="outputDir">Output directory</param>
        /// <returns>File save path</returns>
        public string Save(PluginFileResult result, string outputDir)
        {
            string path = Path.Combine(outputDir, result.Name);
            File.WriteAllBytes(path, result.Content);
            return path;
        }

        /// <summary>
        /// Prints result Text property to the console
        /// </summary>
        /// <param name="result">Plugin text result</param>
        public void Print(PluginTextResult result)
        {
            Console.WriteLine(result.Text);
        }
    }
}
