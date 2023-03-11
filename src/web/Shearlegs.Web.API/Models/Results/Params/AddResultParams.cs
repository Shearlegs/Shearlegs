using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.Results.Params
{
    public class AddResultParams
    {
        public int VersionId { get; set; }
        public byte[] ParametersData { get; set; }
        public ResultStatus Status { get; set; }
        public int? UserId { get; set; }
    }
}
