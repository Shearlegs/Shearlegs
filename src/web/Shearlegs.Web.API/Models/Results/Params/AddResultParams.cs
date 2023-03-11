using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.Results.Params
{
    public class AddResultParams
    {
        public int VersionId { get; set; }
        public byte[] ParametersJson { get; set; }
        public ResultStatus Status { get; set; }
        public int? CreateUserId { get; set; }
    }
}
