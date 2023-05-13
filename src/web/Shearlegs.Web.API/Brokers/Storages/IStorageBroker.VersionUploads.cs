using Shearlegs.Web.API.Models.VersionUploads.Params;
using Shearlegs.Web.API.Models.VersionUploads;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shearlegs.Web.API.Models.VersionUploads.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<VersionUpload> GetVersionUploadAsync(GetVersionUploadsParams @params);
        ValueTask<IEnumerable<VersionUpload>> GetVersionUploadsAsync(GetVersionUploadsParams @params);
        ValueTask<AddVersionUploadResult> AddVersionUploadAsync(AddVersionUploadParams @params);
        ValueTask<StoredProcedureResult> StartProcessingVersionUploadAsync(StartProcessingVersionUploadParams @params);
        ValueTask<StoredProcedureResult> FinishProcessingVersionUploadAsync(FinishProcessingVersionUploadParams @params);
    }
}
