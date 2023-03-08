using Shearlegs.Web.API.Utilities.StoredProcedures;

namespace Shearlegs.Web.API.Models.PluginSecrets.Results
{
    public class AddPluginSecretResult
    {
        public StoredProcedureResult StoredProcedureResult { get; set; }
        public int? PluginSecretId { get; set; }
    }
}
