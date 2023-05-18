namespace Shearlegs.Core.Plugins.Result
{
    public class PluginTextResult : PluginResult
    {        
        public string Text { get; set; }
        public bool IsMarkupString { get; set; }
        public override string ResultType => "Text";

        public PluginTextResult(string text, bool isMarkupString)
        {
            Text = text;
            IsMarkupString = isMarkupString;
        }
    }
}
