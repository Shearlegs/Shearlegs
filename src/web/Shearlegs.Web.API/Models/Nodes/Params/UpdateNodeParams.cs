namespace Shearlegs.Web.API.Models.Nodes.Params
{
    public class UpdateNodeParams
    {
        public int NodeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FQDN { get; set; }
        public string Scheme { get; set; }
        public int HttpPort { get; set; }
        public int HttpsPort { get; set; }
        public int CacheSizeLimit { get; set; }
        public bool IsBehindProxy { get; set; }
        public bool IsEnabled { get; set; }
        public int? UpdateUserId { get; set; }
    }
}
