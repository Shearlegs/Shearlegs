using Shearlegs.Web.API.Utilities.StoredProcedures;

namespace Shearlegs.Web.API.Models.Plugins.Results
{
    public class AddPluginResult
    {
        public StoredProcedureResult StoredProcedureResult { get; set; }
        public int? PluginId { get; set; }
    }
}
