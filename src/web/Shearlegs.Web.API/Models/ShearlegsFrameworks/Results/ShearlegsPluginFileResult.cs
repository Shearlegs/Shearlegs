namespace Shearlegs.Web.API.Models.ShearlegsFrameworks.Results
{
    public class ShearlegsPluginFileResult : ShearlegsPluginResult
    {
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }
    }
}
