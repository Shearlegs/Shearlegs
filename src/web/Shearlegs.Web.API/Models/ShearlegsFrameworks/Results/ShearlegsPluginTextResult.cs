namespace Shearlegs.Web.API.Models.ShearlegsFrameworks.Results
{
    public class ShearlegsPluginTextResult : ShearlegsPluginResult
    {
        public override string ResultType => "Text";

        public string Text { get; set; }
        public bool IsMarkupString { get; set; }
    }
}
