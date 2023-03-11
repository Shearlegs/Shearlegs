using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.Results.Params
{
    public class UpdateResultStatusParams
    {
        public int ResultId { get; set; }
        public ResultStatus Status { get; set; }
    }
}
