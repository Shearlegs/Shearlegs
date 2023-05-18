namespace Shearlegs.Core.Plugins.Result
{
    public class PluginFileResult : PluginResult
    {
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }
        public override string ResultType => "File";

        public PluginFileResult(string name, string mimeType, byte[] content)
        {
            Name = name;
            MimeType = mimeType;
            Content = content;
        }
    }
}
