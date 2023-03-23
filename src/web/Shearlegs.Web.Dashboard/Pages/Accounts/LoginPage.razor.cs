using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.UserAuthentications;
using Shearlegs.Web.APIClient.Models.UserAuthentications.Requests;

namespace Shearlegs.Web.Dashboard.Pages.Accounts
{
    public partial class LoginPage
    {
        public LoginUserRequest LoginUserRequest { get; set; } = new();
        public ShearlegsWebAPIRequestException LoginUserRequestException { get; set; }
        private bool LoginUserRequestProcessing { get; set; }

        private async Task HandleLoginAsync()
        {
            LoginUserRequestException = null;
            LoginUserRequestProcessing = true;

            try
            {
                UserAuthenticationToken authenticationToken = await client.UserAuthentication.LoginUserAsync(LoginUserRequest);

                client.UpdateAuthorization(authenticationToken.Token);
                userState.SetAuthenticatedUser(authenticationToken.AuthenticatedUser);
                DateTimeOffset expireDate = authenticationToken.AuthenticatedUser.UserSession.ExpireDate;
                await cookieBroker.SetValue("JWT", authenticationToken.Token, expireDate);

                navigationManager.NavigateTo("/");
            } catch (ShearlegsWebAPIRequestException exception)
            {
                LoginUserRequestException = exception;
            }

            LoginUserRequestProcessing = false;
        }
    }
}
