namespace Shearlegs.Web.API.Models.NodeDaemons
{
    public class NodeDaemon
    {
        public string NodeVersion { get; set; }
        public string ShearlegsRuntimeVersion { get; set; }
        public long CacheSizeBytes { get; set; }
    }
}
