using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Shearlegs.Web.Dashboard.Shared.Layouts
{
    public partial class MainLayout
    {
        private bool isDarkTheme = false;
        private bool isDrawerOpen = true;

        protected override void OnInitialized()
        {
            snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;   
        }

        Task DrawerToggle()
        {
            isDrawerOpen = !isDrawerOpen;

            return Task.CompletedTask;
        }

        Task ToggleDarkTheme()
        {
            isDarkTheme = !isDarkTheme;

            return Task.CompletedTask;
        }

        async Task HandleLogout(MouseEventArgs args)
        {
            snackbar.Add("handle logout");
            await authenticationService.LogoutAsync();
            
            navigationManager.NavigateTo("/account/login");
        }
    }
}