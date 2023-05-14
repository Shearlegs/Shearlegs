using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Models.VersionUploads.Exceptions;
using Shearlegs.Web.API.Models.VersionUploads.Params;
using Shearlegs.Web.API.Models.VersionUploads.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.VersionUploads
{
    public class VersionUploadService : IVersionUploadService
    {
        private readonly IStorageBroker storageBroker;

        public VersionUploadService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<VersionUpload> RetrieveVersionUploadByIdAsync(int versionUploadId)
        {
            GetVersionUploadsParams @params = new()
            {
                VersionUploadId = versionUploadId
            };

            VersionUpload versionUpload = await storageBroker.GetVersionUploadAsync(@params);

            if (versionUpload == null)
            {
                throw new NotFoundVersionUploadException();
            }

            return versionUpload;
        }

        public async ValueTask<IEnumerable<VersionUpload>> RetrieveAllVersionUploadsAsync()
        {
            GetVersionUploadsParams @params = new();

            IEnumerable<VersionUpload> versionUploads = await storageBroker.GetVersionUploadsAsync(@params);

            return versionUploads;
        }

        public async ValueTask<IEnumerable<VersionUpload>> RetrieveVersionUploadByUserIdAsync(int userId)
        {
            GetVersionUploadsParams @params = new()
            {
                UserId = userId
            };

            IEnumerable<VersionUpload> versionUploads = await storageBroker.GetVersionUploadsAsync(@params);

            return versionUploads;
        }

        public async ValueTask<VersionUploadContent> RetrieveVersionUploadContentByIdAsync(int versionUploadId)
        {
            VersionUploadContent versionUplaodContent = await storageBroker.SelectVersionUploadContentByIdAsync(versionUploadId);

            if (versionUplaodContent == null)
            {
                throw new NotFoundVersionUploadException();
            }

            return versionUplaodContent;
        }

        public async ValueTask<VersionUpload> AddVersionUploadAsync(AddVersionUploadParams @params)
        {
            AddVersionUploadResult result = await storageBroker.AddVersionUploadAsync(@params);

            return await RetrieveVersionUploadByIdAsync(result.VersionUploadId.GetValueOrDefault());
        }

        public async ValueTask<VersionUpload> StartProcessingVersionUploadAsync(StartProcessingVersionUploadParams @params)
        {
            StoredProcedureResult result = await storageBroker.StartProcessingVersionUploadAsync(@params);
            if (result.ReturnValue == 1)
            {
                throw new NotFoundVersionUploadException();
            }

            return await RetrieveVersionUploadByIdAsync(@params.VersionUploadId);
        }

        public async ValueTask<VersionUpload> FinishProcessingVersionUploadAsync(FinishProcessingVersionUploadParams @params)
        {
            StoredProcedureResult result = await storageBroker.FinishProcessingVersionUploadAsync(@params);
            if (result.ReturnValue == 1)
            {
                throw new NotFoundVersionUploadException();
            }

            return await RetrieveVersionUploadByIdAsync(@params.VersionUploadId);
        }
    }
}
