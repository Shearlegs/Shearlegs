namespace Shearlegs.Web.API.Models.ShearlegsFrameworks.Results
{
    public class ShearlegsPluginFileResult : ShearlegsPluginResult
    {
        public override string ResultType => "File";

        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }
    }
}
