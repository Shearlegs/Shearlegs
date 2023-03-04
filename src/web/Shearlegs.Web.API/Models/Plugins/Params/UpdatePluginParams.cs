namespace Shearlegs.Web.API.Models.Plugins.Params
{
    public class UpdatePluginParams
    {
        public int PluginId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int? UpdateUserId { get; set; }
    }
}
