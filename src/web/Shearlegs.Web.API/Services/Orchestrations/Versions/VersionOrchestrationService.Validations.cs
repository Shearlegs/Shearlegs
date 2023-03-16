using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shearlegs.Web.API.Models.Versions.Exceptions;
using Shearlegs.Web.API.Models.Versions.Params;

namespace Shearlegs.Web.API.Services.Orchestrations.Versions
{
    public partial class VersionOrchestrationService
    {
        private void ValidateExecuteVersionParams(ExecuteVersionParams @params)
        {
            ExecuteVersionParamsValidationException validationException = new();

            try
            {
                JObject.Parse(@params.ParametersJson);
            } catch (JsonReaderException exception)
            {
                validationException.UpsertDataList("ParametersJson", exception.Message);
            }

            validationException.ThrowIfContainsErrors();
        }
    }
}
