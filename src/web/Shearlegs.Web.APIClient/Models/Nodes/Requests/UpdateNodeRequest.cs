namespace Shearlegs.Web.APIClient.Models.Nodes.Requests
{
    public class UpdateNodeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FQDN { get; set; }
        public string Scheme { get; set; }
        public bool IsBehindProxy { get; set; }
        public bool IsEnabled { get; set; }
    }
}
