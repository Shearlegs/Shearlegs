using Shearlegs.Web.APIClient.Models.UserAuthentications.Requests;
using Shearlegs.Web.APIClient.Models.Users;
using Shearlegs.Web.APIClient.Models.Users.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Services.Users
{
    public class UsersAPIService
    {
        private readonly ShearlegsWebAPIClient client;

        public UsersAPIService(ShearlegsWebAPIClient client)
        {
            this.client = client;
        }

        public async ValueTask<List<User>> GetAllUsersAsync()
        {
            string requestUri = "/users";

            return await client.GetFromJsonAsync<List<User>>(requestUri);
        }

        public async ValueTask<User> GetUserByNameAsync(string username)
        {
            string requestUri = $"/users/name/{username}";

            return await client.GetFromJsonAsync<User>(requestUri);
        }

        public async ValueTask<User> GetUserByIdAsync(int userId)
        {
            string requestUri = $"/users/{userId}";

            return await client.GetFromJsonAsync<User>(requestUri);
        }

        public async ValueTask<User> CreateUserAsync(CreateUserRequest request)
        {
            string requestUri = "/users/create";

            return await client.PostAsJsonAsync<User>(requestUri, request);
        }
    }
}
