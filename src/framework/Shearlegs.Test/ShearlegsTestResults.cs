using Shearlegs.Core.Plugins.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Test
{
    public class ShearlegsTestResults
    {
        public string Save(PluginFileResult result)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), result.Name);
            File.WriteAllBytes(path, result.Content);
            return path;
        }

        public string Save(PluginFileResult result, string outputDir)
        {
            string path = Path.Combine(outputDir, result.Name);
            File.WriteAllBytes(path, result.Content);
            return path;
        }

        public void Print(PluginTextResult result)
        {
            Console.WriteLine(result.Text);
        }
    }
}
