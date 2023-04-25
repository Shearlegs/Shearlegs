using MudBlazor;
using Shearlegs.Web.APIClient.Models.Plugins;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Plugins
{
    public partial class PluginsManagementPage
    {
        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Management", "/management"),
            new BreadcrumbItem("Plugins", null, true)
        };

        public List<Plugin> Plugins { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Plugins = await client.Plugins.GetAllPluginsAsync();
        }

        private string searchString = string.Empty;

        private bool SearchPlugin(Plugin plugin)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (plugin.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (plugin.PackageId.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (plugin.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (plugin.Author.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (plugin.UpdateUser?.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                return true;
            if (plugin.CreateUser?.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                return true;
            return false;
        }
    }
}
