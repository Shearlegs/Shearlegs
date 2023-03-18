using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Users;
using Shearlegs.Web.APIClient.Models.Users.Requests;

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
                User user = await client.Users.LoginUserAsync(LoginUserRequest);
                userState.SetUser(user);

                navigationManager.NavigateTo("/");
            } catch (ShearlegsWebAPIRequestException exception)
            {
                LoginUserRequestException = exception;
            }

            LoginUserRequestProcessing = false;
        }
    }
}
