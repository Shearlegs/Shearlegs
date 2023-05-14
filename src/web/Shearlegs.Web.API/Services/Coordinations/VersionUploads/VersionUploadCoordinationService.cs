using Microsoft.AspNetCore.Http;
using NuGet.Versioning;
using Shearlegs.Web.API.Models.NodeDaemons;
using Shearlegs.Web.API.Models.NodeDaemons.Exceptions;
using Shearlegs.Web.API.Models.NodeDaemons.Params;
using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Models.VersionUploads.Params;
using Shearlegs.Web.API.Services.Orchestrations.Nodes;
using Shearlegs.Web.API.Services.Orchestrations.Schedulings;
using Shearlegs.Web.API.Services.Orchestrations.VersionUploads;
using Shearlegs.Web.Shared.Enums;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async ValueTask QueueProcessVersionAsync(int versionUploadId)
        {
            await schedulingService.EnqueueAsync(() => ProcessVersionUploadAsync(versionUploadId));
        }

        private async Task ProcessVersionUploadAsync(int versionUploadId)
        {
            IEnumerable<Node> nodes = await nodeService.RetrieveAllNodesAsync();
            int nodeId = nodes.First().Id;

            VersionUpload versionUpload = await versionUploadService.RetrieveVersionUploadByIdAsync(versionUploadId);

            // check if version upload is already being processed or has already been processed

            StartProcessingVersionUploadParams startProcessingVersionUploadParams = new()
            {
                VersionUploadId = versionUploadId,
                NodeId = nodeId,
                Status = VersionUploadStatus.Processing
            };

            versionUpload = await versionUploadService.StartProcessingVersionUploadAsync(startProcessingVersionUploadParams);

            VersionUploadContent versionUploadContent = await versionUploadService.RetrieveVersionUploadContentByIdAsync(versionUploadId);

            using (MemoryStream ms = new(versionUploadContent.Content))
            {
                ProcessPluginParams processPluginParams = new()
                {
                    PluginFile = new FormFile(ms, 0, ms.Length, "formFile", versionUploadContent.FileName)
                };

                
                try
                {
                    ProcessedPluginInfo processedPluginInfo = await nodeService.ProcessPluginAsync(nodeId, processPluginParams);

                    FinishProcessingVersionUploadParams finishProcessingVersionUploadParams = new()
                    {
                        VersionUploadId = versionUpload.Id,
                        Status = VersionUploadStatus.Completed,
                        PackageId = processedPluginInfo.PackageId,
                        PackageVersion = processedPluginInfo.Version
                    };

                    versionUpload = await versionUploadService.FinishProcessingVersionUploadAsync(finishProcessingVersionUploadParams);

                } catch (NodeDaemonCommunicationException exception)
                {
                    FinishProcessingVersionUploadParams finishProcessingVersionUploadParams = new()
                    {
                        VersionUploadId = versionUpload.Id,
                        Status = VersionUploadStatus.Failed,
                        ErrorMessage = exception.Message
                    };

                    versionUpload = await versionUploadService.FinishProcessingVersionUploadAsync(finishProcessingVersionUploadParams);
                }
            }
        }
    }
}
