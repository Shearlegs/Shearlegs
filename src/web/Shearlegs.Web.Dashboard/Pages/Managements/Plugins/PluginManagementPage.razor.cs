using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Plugins;
using Shearlegs.Web.APIClient.Models.Plugins.Requests;
using Shearlegs.Web.Dashboard.Models.Forms.Managements.Plugins;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Plugins
{
    public partial class PluginManagementPage
    {
        [Parameter]
        public string PackageId { get; set; }

        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Management", "/management"),
            new BreadcrumbItem("Plugins", "/management/plugins")
        };

        public Plugin Plugin { get; set; }
        public UpdatePluginFormModel Model { get; set; }

        private bool isLoaded = false;
        private bool isCanceled = false;
        private bool isUpdateUserProcessing = false;
        private bool showSuccessAlert = false;
        private bool showErrorAlert = false;

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Plugin = await client.Plugins.GetPluginByPackageIdAsync(PackageId);
            } catch (ShearlegsWebAPIRequestException)
            {
                isCanceled = true;
                BreadcrumbItems.Add(new BreadcrumbItem("Not Found", null, true));
                return;
            }

            BreadcrumbItems.Add(new BreadcrumbItem(Plugin.PackageId, null, true));

            Model = new()
            {
                Name = Plugin.Name,
                Description = Plugin.Description,
                Author = Plugin.Author
            };

            isLoaded = true;
        }

        private async Task HandleUpdatePluginAsync()
        {
            isUpdateUserProcessing = true;
            showSuccessAlert = false;
            showErrorAlert = false;

            UpdatePluginRequest request = new()
            {
                Name = Model.Name,
                Description = Model.Description,
                Author = Model.Author
            };

            try
            {
                Plugin = await client.Plugins.UpdatePluginAsync(Plugin.Id, request);
                showSuccessAlert = true;
            } catch (ShearlegsWebAPIRequestException)
            {
                showErrorAlert = true;
            }
            
            isUpdateUserProcessing = false;
        }
    }
}
