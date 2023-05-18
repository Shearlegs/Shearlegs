using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Models.Plugins.Exceptions;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Exceptions;
using Shearlegs.Web.API.Models.Versions.Params;
using Shearlegs.Web.API.Models.Versions.Results;
using Shearlegs.Web.API.Models.VersionUploads.Exceptions;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Versions
{
    public class VersionService : IVersionService
    {
        private readonly IStorageBroker storageBroker;

        public VersionService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<VersionContent> RetrieveVersionContentByIdAsync(int versionId)
        {
            VersionContent versionContent = await storageBroker.SelectVersionContentByIdAsync(versionId);

            if (versionContent == null)
            {
                throw new NotFoundVersionException();
            }

            return versionContent;
        }

        public async ValueTask<Version> RetrieveVersionByIdAsync(int versionId)
        {
            GetVersionsParams @params = new()
            {
                VersionId = versionId
            };

            Version version = await storageBroker.GetVersionAsync(@params);

            if (version == null)
            {
                throw new NotFoundVersionException();
            }

            return version;
        }

        public async ValueTask<IEnumerable<Version>> RetrieveVersionsByPluginIdAsync(int pluginId)
        {
            GetVersionsParams @params = new()
            {
                PluginId = pluginId
            };

            return await storageBroker.GetVersionsAsync(@params);
        }

        public async ValueTask<IEnumerable<Version>> RetrieveAllVersionsAsync()
        {
            GetVersionsParams @params = new();

            return await storageBroker.GetVersionsAsync(@params);
        }

        public async ValueTask<Version> AddVersionAsync(AddVersionParams @params)
        {
            @params.ContentLength = @params.Content.Length;

            AddVersionResult result = await storageBroker.AddVersionAsync(@params);            

            if (result.StoredProcedureResult.ReturnValue == 1)
            {
                throw new NotFoundPluginException();
            }

            if (result.StoredProcedureResult.ReturnValue == 2)
            {
                throw new AlreadyExistsVersionException();
            }

            return await RetrieveVersionByIdAsync(result.VersionId.Value);
        }

        public async ValueTask<Version> MigrateVersionUploadToVersionAsync(int versionUploadId)
        {
            MigrateVersionUploadToVersionResult result = await storageBroker.MigrateVersionUploadToVersionAsync(versionUploadId);

            if (result.StoredProcedureResult.ReturnValue == 1)
            {
                throw new NotFoundVersionUploadException();
            }

            if (result.StoredProcedureResult.ReturnValue == 2)
            {
                throw new NotFoundPluginException();
            }

            if (result.StoredProcedureResult.ReturnValue == 3)
            {
                throw new AlreadyExistsVersionException();
            }

            return await RetrieveVersionByIdAsync(result.VersionId.GetValueOrDefault());
        }
    }
}
