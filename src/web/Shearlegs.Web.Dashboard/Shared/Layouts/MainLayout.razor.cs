namespace Shearlegs.Web.Dashboard.Shared.Layouts
{
    public partial class MainLayout
    {
        private bool isDarkTheme = false;
        private bool isDrawerOpen = true;
        
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
    }
}