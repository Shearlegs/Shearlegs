using System.IO;

namespace Shearlegs.Web.APIClient.Models
{
    public class FormFile
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Stream Stream { get; set; }
    }
}
