namespace Shearlegs.Web.API.Models.Versions.Params
{
    public class ExecuteVersionParams
    {
        public int VersionId { get; set; }
        public string ParametersJson { get; set; }
        public int? UserId { get; set; }
    }
}
