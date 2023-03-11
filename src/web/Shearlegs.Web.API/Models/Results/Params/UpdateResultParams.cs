using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.Results.Params
{
    public class UpdateResultParams
    {
        public int ResultId { get; set; }
        public ResultStatus Status { get; set; }
        public byte[] ResultData { get; set; }
        public string ResultType { get; set; }
    }
}
