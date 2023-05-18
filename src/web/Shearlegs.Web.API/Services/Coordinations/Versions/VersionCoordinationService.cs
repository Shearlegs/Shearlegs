using Shearlegs.Web.API.Services.Orchestrations.Nodes;
using Shearlegs.Web.API.Services.Orchestrations.Schedulings;
using Shearlegs.Web.API.Services.Orchestrations.Versions;

namespace Shearlegs.Web.API.Services.Coordinations.Versions
{
    public class VersionCoordinationService : IVersionCoordinationService
    {
        private readonly IVersionOrchestrationService versionService;
        private readonly INodeOrchestrationService nodeService;
        private readonly ISchedulingOrchestrationService schedulingService;

        public VersionCoordinationService(IVersionOrchestrationService versionService, INodeOrchestrationService nodeService, ISchedulingOrchestrationService schedulingService)
        {
            this.versionService = versionService;
            this.nodeService = nodeService;
            this.schedulingService = schedulingService;
        }


    }
}
