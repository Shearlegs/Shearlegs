using Microsoft.AspNetCore.Http;
using Shearlegs.Web.APIClient.Models;
using Shearlegs.Web.APIClient.Models.VersionUploads;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Services.VersionUploadsAPI
{
    public class VersionUploadsAPIService
    {
        private readonly ShearlegsWebAPIClient client;

        public VersionUploadsAPIService(ShearlegsWebAPIClient client)
        {
            this.client = client;
        }

        public async ValueTask<VersionUpload> GetVersionUploadByIdAsync(int versionUploadId)
        {
            string requestUri = $"/versionuploads/{versionUploadId}";

            return await client.GetFromJsonAsync<VersionUpload>(requestUri);
        }

        public async ValueTask<List<VersionUpload>> GetAllVersionUploadsAsync()
        {
            string requestUri = "/versionuploads";

            return await client.GetFromJsonAsync<List<VersionUpload>>(requestUri);
        }

        public async ValueTask<List<VersionUpload>> getVersionUploadsByUserIdAsync(int userId)
        {
            string requestUri = $"/versionuploads/userid/{userId}";

            return await client.GetFromJsonAsync<List<VersionUpload>>(requestUri);
        }

        public async ValueTask<VersionUpload> AddVersionUploadAsync(IFormFile formFile)
        {
            string requestUri = $"/versionuploads/add";

            HttpResponseMessage responseMessage  = await client.PostFileAsync(requestUri, formFile);
            
            return await responseMessage.Content.ReadFromJsonAsync<VersionUpload>();
        }
    }
}
