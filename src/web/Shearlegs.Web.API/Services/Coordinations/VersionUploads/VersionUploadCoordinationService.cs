using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Services.Orchestrations.Nodes;
using Shearlegs.Web.API.Services.Orchestrations.Schedulings;
using Shearlegs.Web.API.Services.Orchestrations.VersionUploads;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Coordinations.VersionUploads
{
    public class VersionUploadCoordinationService : IVersionUploadCoordinationService
    {
        private readonly IVersionUploadOrchestrationService versionUploadService;
        private readonly INodeOrchestrationService nodeService;
        private readonly ISchedulingOrchestrationService schedulingService;

        public VersionUploadCoordinationService(
            IVersionUploadOrchestrationService versionUploadService, 
            INodeOrchestrationService nodeService, 
            ISchedulingOrchestrationService schedulingService)
        {
            this.versionUploadService = versionUploadService;
            this.nodeService = nodeService;
            this.schedulingService = schedulingService;
        }

        public async ValueTask ProcessVersionUploadAsync(int versionUploadId)
        {

        }
    }
}
