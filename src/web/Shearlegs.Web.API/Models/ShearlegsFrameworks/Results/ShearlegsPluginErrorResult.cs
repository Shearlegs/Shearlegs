namespace Shearlegs.Web.API.Models.ShearlegsFrameworks.Results
{
    public class ShearlegsPluginErrorResult : ShearlegsPluginResult
    {
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionString { get; set; }
    }
}
