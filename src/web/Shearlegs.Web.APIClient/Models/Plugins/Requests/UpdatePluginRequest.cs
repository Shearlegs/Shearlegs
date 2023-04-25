namespace Shearlegs.Web.APIClient.Models.Plugins.Requests
{
    public class UpdatePluginRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int? UpdateUserId { get; set; }
    }
}
