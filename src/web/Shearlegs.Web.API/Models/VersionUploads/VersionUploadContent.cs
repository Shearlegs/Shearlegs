using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Shearlegs.Web.API.Models.VersionUploads
{
    public class VersionUploadContent
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
