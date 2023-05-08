using System;

namespace Shearlegs.Web.API.Models.VersionUploads.Params
{
    public class AddVersionUploadParams
    {
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public long ContentLength { get; set; }
        public DateTimeOffset? LastModified { get; set; }
    }
}
