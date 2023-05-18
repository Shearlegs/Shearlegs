using Shearlegs.Web.APIClient.Models.Versions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Services.VersionsAPI
{
    public class VersionsAPIService
    {
        private readonly ShearlegsWebAPIClient client;

        public VersionsAPIService(ShearlegsWebAPIClient client)
        {
            this.client = client;
        }

        public async ValueTask<Version> GetVersionByIdAsync(int versionId)
        {
            return await client.GetFromJsonAsync<Version>($"versions/{versionId}");
        }

        public async ValueTask<List<Version>> GetAllVersionsAsync()
        {
            return await client.GetFromJsonAsync<List<Version>>("versions");
        }
    }
}
