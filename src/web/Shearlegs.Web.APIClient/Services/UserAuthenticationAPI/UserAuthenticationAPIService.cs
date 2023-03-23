using Shearlegs.Web.APIClient.Models.UserAuthentications;
using Shearlegs.Web.APIClient.Models.UserAuthentications.Requests;
using Shearlegs.Web.APIClient.Models.Users;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Services.UserAuthenticationAPI
{
    public class UserAuthenticationAPIService
    {
        private readonly ShearlegsWebAPIClient client;

        public UserAuthenticationAPIService(ShearlegsWebAPIClient client)
        {
            this.client = client;
        }

        public async ValueTask<AuthenticatedUser> GetAuthenticatedUserAsync()
        {
            string requestUri = "/users/authentication";

            return await client.GetFromJsonAsync<AuthenticatedUser>(requestUri);
        }

        public async ValueTask<UserAuthenticationToken> LoginUserAsync(LoginUserRequest request)
        {
            string requestUri = "/users/authentication/login";

            return await client.PostAsJsonAsync<UserAuthenticationToken>(requestUri, request);
        }

        public async ValueTask LogoutUserAsync()
        {
            string requestUri = "/users/authentication/logout";

            await client.PostAsync(requestUri, null);
        }
    }
}
