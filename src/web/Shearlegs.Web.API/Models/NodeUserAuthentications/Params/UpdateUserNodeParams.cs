namespace Shearlegs.Web.API.Models.NodeUserAuthentications.Params
{
    public class UpdateUserNodeParams
    {
        public int NodeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FQDN { get; set; }
        public string Scheme { get; set; }
        public bool IsBehindProxy { get; set; }
        public bool IsEnabled { get; set; }
    }
}
