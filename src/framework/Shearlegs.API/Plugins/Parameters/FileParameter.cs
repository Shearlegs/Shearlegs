namespace Shearlegs.API.Plugins.Parameters
{
    public class FileParameter
    {
        public string Name { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }
    }
}
