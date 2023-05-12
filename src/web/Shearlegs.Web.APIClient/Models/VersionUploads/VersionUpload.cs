using Shearlegs.Web.APIClient.Models.Nodes;
using Shearlegs.Web.APIClient.Models.Users;
using System;

namespace Shearlegs.Web.APIClient.Models.VersionUploads
{
    public class VersionUpload
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public byte Status { get; set; }
        public string PackageId { get; set; }
        public string PackageVersion { get; set; }
        public string ErrorMessage { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo User { get; set; }
        public NodeInfo Node { get; set; }
    }
}
