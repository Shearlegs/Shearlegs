using Shearlegs.Web.APIClient.Models.Exceptions;
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
                User user = await client.Account.GetUserAsync();
            } catch (ShearlegsWebAPIRequestException exception)
            {
                Console.WriteLine(exception.Message);
            }

            IsInitialized = true;
        }
    }
}
