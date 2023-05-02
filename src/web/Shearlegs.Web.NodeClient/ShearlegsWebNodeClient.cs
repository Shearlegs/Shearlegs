using Shearlegs.Web.NodeClient.Models;
using Shearlegs.Web.NodeClient.Models.Exceptions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shearlegs.Web.NodeClient
{
    public class ShearlegsWebNodeClient
    {
        private readonly HttpClient httpClient;

        public ShearlegsWebNodeClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async ValueTask<NodeInfo> GetNodeInfoAsync()
        {
            return await GetFromJsonAsync<NodeInfo>("info");
        }

        public async ValueTask<NodeFullInfo> GetNodeFullInfoAsync()
        {
            return await GetFromJsonAsync<NodeFullInfo>("fullinfo");
        }

        public async ValueTask<T> GetFromJsonAsync<T>(string requestUri)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<T>(requestUri);
            } catch (HttpRequestException exception)
            {
                throw new ShearlegsWebNodeClientRequestException(exception.Message, exception.StatusCode);
            }            
        }
    }
}