namespace Shearlegs.Web.APIClient.Models.Nodes
{
    public class NodeDaemon
    {
        public string NodeVersion { get; set; }
        public string ShearlegsRuntimeVersion { get; set; }
        public long CacheSizeBytes { get; set; }
    }
}
