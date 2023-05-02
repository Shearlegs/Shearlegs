using System.IO;

namespace Shearlegs.Web.Node.Helpers
{
    public class DirectoryHelper
    {
        public static long GetDirectorySize(string directoryPath)
        {
            DirectoryInfo directoryInfo = new(directoryPath);
            
            return GetDirectorySize(directoryInfo);
        }

        public static long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;

            // Add file sizes.
            FileInfo[] files = directoryInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }

            // Add subdirectory sizes.
            DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();
            foreach (DirectoryInfo subDirectory in subDirectories)
            {
                size += GetDirectorySize(subDirectory);
            }

            return size;
        }
    }
}
