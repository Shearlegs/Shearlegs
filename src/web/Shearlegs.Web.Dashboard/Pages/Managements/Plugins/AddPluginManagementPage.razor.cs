using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Plugins;
using Shearlegs.Web.APIClient.Models.Plugins.Requests;
using Shearlegs.Web.Dashboard.Models.Forms.Managements.Plugins;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Plugins
{
    public partial class AddPluginManagementPage
    {
        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Management", "/management"),
            new BreadcrumbItem("Plugins", "/management/plugins"),
            new BreadcrumbItem("Add", null, true)
        };

        public AddPluginFormModel Model { get; set; } = new();

        private bool isAddPluginProcessing = false;
        private bool showSuccessAlert = false;
        private bool showErrorAlert = false;

        private async Task HandleAddPluginAsync()
        {
            isAddPluginProcessing = true;
            showSuccessAlert = false;
            showErrorAlert = false;

            AddPluginRequest request = new()
            {
                PackageId = Model.PackageId,
                Name = Model.Name,
                Description = Model.Description,
                Author = Model.Author
            };

            try
            {
                Plugin plugin = await client.Plugins.AddPluginAsync(request);
                showSuccessAlert = true;

                navigationManager.NavigateTo($"/management/plugins/{plugin.PackageId}");
            }
            catch (ShearlegsWebAPIRequestException)
            {
                showErrorAlert = true;
            }

            isAddPluginProcessing = false;
        }
    }
}
