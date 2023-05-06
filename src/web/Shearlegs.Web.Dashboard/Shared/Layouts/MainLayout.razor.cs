using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Threading.Tasks;

namespace Shearlegs.Web.Dashboard.Shared.Layouts
{
    public partial class MainLayout
    {
        private bool isDarkTheme = false;
        private bool isDrawerOpen = true;

        protected override void OnInitialized()
        {
            snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            snackbar.Configuration.SnackbarVariant = Variant.Outlined;
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
            await authenticationService.LogoutAsync();
            snackbar.Add("Successfully logged you out!", Severity.Success);

            navigationManager.NavigateTo("/account/login");
        }
    }
}