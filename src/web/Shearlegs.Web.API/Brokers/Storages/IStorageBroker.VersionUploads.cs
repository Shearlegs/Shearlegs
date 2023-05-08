using Shearlegs.Web.API.Models.VersionUploads.Params;
using Shearlegs.Web.API.Models.VersionUploads;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shearlegs.Web.API.Models.VersionUploads.Results;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<VersionUpload> GetVersionUploadAsync(GetVersionUploadsParams @params);
        ValueTask<IEnumerable<VersionUpload>> GetVersionUploadsAsync(GetVersionUploadsParams @params);
        ValueTask<AddVersionUploadResult> AddVersionUploadAsync(AddVersionUploadParams @params);
    }
}
