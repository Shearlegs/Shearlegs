using Microsoft.AspNetCore.Components;
using Shearlegs.Web.APIClient;
using Shearlegs.Web.APIClient.Models.UserAuthentications;
using Shearlegs.Web.APIClient.Models.UserAuthentications.Requests;
using Shearlegs.Web.Dashboard.Brokers.Cookies;

namespace Shearlegs.Web.Dashboard.Services
{
    public class AuthenticationService
    {
        private readonly UserState userState;
        private readonly ShearlegsWebAPIClient client;
        private readonly ICookieBroker cookieBroker;

        public AuthenticationService(
            UserState userState, 
            ShearlegsWebAPIClient client, 
            ICookieBroker cookieBroker)
        {
            this.userState = userState;
            this.client = client;
            this.cookieBroker = cookieBroker;
        }

        public async ValueTask LoginAsync(string username, string password)
        {
            LoginUserRequest request = new()
            {
                Username = username,
                Password = password
            };

            UserAuthenticationToken authenticationToken = await client.UserAuthentication.LoginUserAsync(request);

            client.UpdateAuthorization(authenticationToken.Token);
            userState.SetAuthenticatedUser(authenticationToken.AuthenticatedUser);
            DateTimeOffset expireDate = authenticationToken.AuthenticatedUser.UserSession.ExpireDate;
            await cookieBroker.SetValue("JWT", authenticationToken.Token, expireDate);
        }

        public async ValueTask LogoutAsync()
        {
            await client.UserAuthentication.LogoutUserAsync();

            client.UpdateAuthorization(null);
            userState.SetAuthenticatedUser(null);

            DateTimeOffset expireDate = DateTimeOffset.UtcNow.AddHours(-9999);
            await cookieBroker.SetValue("JWT", string.Empty, expireDate);

        }
    }
}
