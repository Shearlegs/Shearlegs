using Shearlegs.Web.API.Services.Foundations.Versions;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Versions
{
    public class VersionOrchestrationService : IVersionOrchestrationService
    {
        private readonly IVersionService versionService;

        public VersionOrchestrationService(IVersionService versionService)
        {
            this.versionService = versionService;
        }

        public async ValueTask ExecuteVersionAsync()
        {

        }
    }
}
