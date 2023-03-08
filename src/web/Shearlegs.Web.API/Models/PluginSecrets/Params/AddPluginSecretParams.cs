namespace Shearlegs.Web.API.Models.PluginSecrets.Params
{
    public class AddPluginSecretParams
    {
        public int PluginId { get; set; }
        public string Name { get; set; }
        public byte[] Value { get; set; }
        public int? CreateUserId { get; set; }
    }
}
