﻿using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Services.Users;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient
{
    public class ShearlegsWebAPIClient
    {
        private readonly HttpClient httpClient;

        public ShearlegsWebAPIClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;

            Users = new(this);
        }

        public UsersAPIService Users { get; }

        internal async ValueTask<T> GetFromJsonAsync<T>(string requestUri)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<T>(requestUri);
            } catch (HttpRequestException exception)
            {
                throw new ShearlegsWebAPIRequestException(exception.Message);
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
                throw new ShearlegsWebAPIRequestException(exception.Message);
            }
        }
    }
}