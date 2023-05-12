using Microsoft.AspNetCore.Http;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Services.NodesAPI;
using Shearlegs.Web.APIClient.Services.NodeVariablesAPI;
using Shearlegs.Web.APIClient.Services.PluginsAPI;
using Shearlegs.Web.APIClient.Services.UserAuthenticationAPI;
using Shearlegs.Web.APIClient.Services.Users;
using Shearlegs.Web.APIClient.Services.VersionUploadsAPI;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient
{
    public class ShearlegsWebAPIClient
    {
        private readonly HttpClient httpClient;

        public ShearlegsWebAPIClient(HttpClient httpClient, string jwtToken = null)
        {
            this.httpClient = httpClient;

            UpdateAuthorization(jwtToken);

            Users = new(this);
            UserAuthentication = new(this);
            Plugins = new(this);
            Nodes = new(this);
            NodeVariables = new(this);
            VersionUploads = new(this);
        }

        public void UpdateAuthorization(string jwtToken)
        {
            if (!string.IsNullOrEmpty(jwtToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new("Bearer", jwtToken);
            } else
            {
                httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
        }

        public UsersAPIService Users { get; }
        public UserAuthenticationAPIService UserAuthentication { get; }
        public PluginsAPIService Plugins { get; }
        public NodesAPIService Nodes { get; }
        public NodeVariablesAPIService NodeVariables { get; }
        public VersionUploadsAPIService VersionUploads { get; }

        internal async ValueTask<T> GetFromJsonAsync<T>(string requestUri)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<T>(requestUri);
            } catch (HttpRequestException exception)
            {
                throw new ShearlegsWebAPIRequestException(exception.Message, exception.StatusCode);
            }            
        }

        internal async ValueTask<T> PostAsJsonAsync<T>(string requestUri, object value)
        {
            try
            {
                HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(requestUri, value);
                responseMessage.EnsureSuccessStatusCode();

                return await responseMessage.Content.ReadFromJsonAsync<T>();
            } catch (HttpRequestException exception)
            {
                throw new ShearlegsWebAPIRequestException(exception.Message, exception.StatusCode);
            }
        }

        internal async ValueTask PostAsync(string requestUri, object value)
        {
            try
            {
                HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(requestUri, value);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException exception)
            {
                throw new ShearlegsWebAPIRequestException(exception.Message, exception.StatusCode);
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
            } catch (HttpRequestException exception)
            {
                throw new ShearlegsWebAPIRequestException(exception.Message, exception.StatusCode);
            }
        }
    }
}