using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.UserAuthentications;
using Shearlegs.Web.APIClient.Models.UserAuthentications.Requests;
using Shearlegs.Web.Dashboard.Models.Forms;

namespace Shearlegs.Web.Dashboard.Pages.Accounts
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
            } catch (ShearlegsWebAPIRequestException exception)
            {
                LoginUserRequestException = exception;
            }

            navigationManager.NavigateTo("/");
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
