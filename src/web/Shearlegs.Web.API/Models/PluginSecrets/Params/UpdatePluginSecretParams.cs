namespace Shearlegs.Web.API.Models.PluginSecrets.Params
{
    public class UpdatePluginSecretParams
    {
        public int PluginSecretId { get; set; }
        public byte[] Value { get; set; }
        public int? UpdateUserId { get; set; }
    }
}
