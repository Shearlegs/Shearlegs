using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Plugins;
using Shearlegs.Web.APIClient.Models.Versions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Version = Shearlegs.Web.APIClient.Models.Versions.Version;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Plugins
{
    public partial class PluginVersionsManagementPage
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
        public List<Version> Versions { get; set; }

        private bool isLoaded = false;
        private bool isCanceled = false;
        private bool isVersionsLoading = true;
        private string searchString = string.Empty;

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Plugin = await client.Plugins.GetPluginByPackageIdAsync(PackageId);
            }
            catch (ShearlegsWebAPIRequestException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
            {
                isCanceled = true;
                BreadcrumbItems.Add(new BreadcrumbItem("Not Found", null, true));
                return;
            }

            BreadcrumbItems.Add(new BreadcrumbItem(Plugin.PackageId, $"/management/plugins/{Plugin.PackageId}"));
            BreadcrumbItems.Add(new BreadcrumbItem("Versions", null, true));

            _ = RefreshVersionsAsync();

            isLoaded = true;
        }

        private async Task RefreshVersionsAsync()
        {
            isVersionsLoading = true;
            await InvokeAsync(StateHasChanged);
            try
            {
                Versions = await client.Plugins.GetVersionsByPluginIdAsync(Plugin.Id);

            } catch (ShearlegsWebAPIRequestException exception)
            {
                loggingBroker.LogException(exception);
            }
            isVersionsLoading = false;
            await InvokeAsync(StateHasChanged);
        }

        private bool SearchVersion(Version version)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (version.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (version.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (version.CreateUser?.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                return true;
            return false;
        }
    }
}
