using Shearlegs.Web.APIClient.Models.Plugins;
using Shearlegs.Web.APIClient.Models.Plugins.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Services.PluginsAPI
{
    public class PluginsAPIService
    {
        private readonly ShearlegsWebAPIClient client;

        public PluginsAPIService(ShearlegsWebAPIClient client)
        {
            this.client = client;
        }

        public async ValueTask<List<Plugin>> GetAllPluginsAsync()
        {
            string requestUri = "/plugins";

            return await client.GetFromJsonAsync<List<Plugin>>(requestUri);
        }

        public async ValueTask<Plugin> GetPluginByIdAsync(int pluginId)
        {
            string requestUri = $"/plugins/{pluginId}";

            return await client.GetFromJsonAsync<Plugin>(requestUri);
        }

        public async ValueTask<Plugin> GetPluginByPackageIdAsync(string packageId)
        {
            string requestUri = $"/plugins/packageId/{packageId}";

            return await client.GetFromJsonAsync<Plugin>(requestUri);
        }

        public async ValueTask<Plugin> AddPluginAsync(AddPluginRequest request)
        {
            string requestUri = "/plugins/add";

            return await client.PostAsJsonAsync<Plugin>(requestUri, request);
        }

        public async ValueTask<Plugin> UpdatePluginAsync(int pluginId, UpdatePluginRequest request)
        {
            string requestUri = $"/plugins/{pluginId}/update";

            return await client.PostAsJsonAsync<Plugin>(requestUri, request);
        }
    }
}
