using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.UserAuthentications;
using Shearlegs.Web.APIClient.Models.Users;

namespace Shearlegs.Web.Dashboard
{
    public partial class App
    {
        public bool IsInitialized { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                AuthenticatedUser authenticatedUser = await client.UserAuthentication.GetAuthenticatedUserAsync();
                userState.SetAuthenticatedUser(authenticatedUser);
            } catch (ShearlegsWebAPIRequestException exception)
            {
                Console.WriteLine("user is not authenticated");
                Console.WriteLine(exception.Message);
            }

            IsInitialized = true;
        }
    }
}
