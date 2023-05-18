using Microsoft.AspNetCore.Http;
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

        public async ValueTask<NodeStatistics> GetNodeStatisticsAsync()
        {
            return await GetFromJsonAsync<NodeStatistics>("statistics");
        }

        public async ValueTask<PluginInformation> ProcessPluginAsync(IFormFile formFile)
        {
            HttpResponseMessage responseMessage = await PostFileAsync("process-plugin", formFile);

            return await responseMessage.Content.ReadFromJsonAsync<PluginInformation>();
        }

        public async ValueTask<string> ExecutePluginAsync(IFormFile formFile, string parametersJson)
        {
            const string requestUri = "execute-plugin";
            MultipartFormDataContent content = new();

            StreamContent fileStreamContent = new(formFile.OpenReadStream());
            content.Add(fileStreamContent, "formFile", formFile.FileName);

            StringContent parametersContent = new(parametersJson);
            content.Add(parametersContent, "parametersJson");

            try
            {
                HttpResponseMessage responseMessage = await httpClient.PostAsync(requestUri, content);
                responseMessage.EnsureSuccessStatusCode();

                return await responseMessage.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException exception)
            {
                throw new ShearlegsWebNodeClientRequestException(exception.Message, exception.StatusCode);
            }
        }

        internal async ValueTask<T> GetFromJsonAsync<T>(string requestUri)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<T>(requestUri);
            } catch (HttpRequestException exception)
            {
                throw new ShearlegsWebNodeClientRequestException(exception.Message, exception.StatusCode);
            }            
        }

        internal async ValueTask<HttpResponseMessage> PostFileAsync(string requestUri, IFormFile formFile)
        {
            MultipartFormDataContent content = new();
            StreamContent fileStreamContent = new(formFile.OpenReadStream());
            content.Add(fileStreamContent, "formFile", formFile.FileName);

            try
            {
                HttpResponseMessage responseMessage = await httpClient.PostAsync(requestUri, content);
                responseMessage.EnsureSuccessStatusCode();

                return responseMessage;
            }
            catch (HttpRequestException exception)
            {
                throw new ShearlegsWebNodeClientRequestException(exception.Message, exception.StatusCode);
            }
        }
    }
}