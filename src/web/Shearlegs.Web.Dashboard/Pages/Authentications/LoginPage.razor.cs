using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.Dashboard.Models.Forms.Authentications;

namespace Shearlegs.Web.Dashboard.Pages.Authentications
{
    public partial class LoginPage
    {
        public LoginUserFormModel LoginUserFormModel { get; set; } = new();
        public ShearlegsWebAPIRequestException LoginUserRequestException { get; set; }
        private bool LoginProcessing { get; set; }
        public bool LogoutProcessing { get; set; }

        private async Task HandleLoginAsync()
        {
            LoginUserRequestException = null;
            LoginProcessing = true;

            try
            {
                await authenticationService.LoginAsync(LoginUserFormModel.Username, LoginUserFormModel.Password);

                navigationManager.NavigateTo("/");
                snackbar.Add("Successfully logged you in!", Severity.Success);
            } catch (ShearlegsWebAPIRequestException exception)
            {
                LoginUserRequestException = exception;
            }
            
            LoginProcessing = false;
        }

        private async Task HandleLogoutAsync()
        {
            LogoutProcessing = true;

            await authenticationService.LogoutAsync();

            LogoutProcessing = false;
        }
    }
}
