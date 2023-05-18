using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Plugins;
using Shearlegs.Web.APIClient.Models.Versions;
using Shearlegs.Web.APIClient.Models.VersionUploads;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.Dashboard.Pages.Users.VersionUploads
{
    public partial class VersionUploadUserPage
    {
        [Parameter]
        public int VersionUploadId { get; set; }

        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("User", "/user"),
            new BreadcrumbItem("Version Uploads", "/user/versionuploads")
        };

        public VersionUpload VersionUpload { get; set; }
        public Version Version { get; set; }
        public Plugin Plugin { get; set; }

        private bool isLoaded = false;
        private bool isCanceled = false;

        protected override async Task OnParametersSetAsync()
        {
            await Refresh();
        }

        private async Task Refresh()
        {
            isLoaded = false;

            while (BreadcrumbItems.Count > 3)
            {
                BreadcrumbItems.RemoveAt(3);
            }

            try
            {
                VersionUpload = await client.VersionUploads.GetVersionUploadByIdAsync(VersionUploadId);
            }
            catch (ShearlegsWebAPIRequestException exception)
            {
                loggingBroker.LogException(exception);

                isCanceled = true;
                BreadcrumbItems.Add(new BreadcrumbItem("Not Found", null, true));
                return;
            }

            try
            {
                if (VersionUpload.VersionId.HasValue)
                {
                    Version = await client.Versions.GetVersionByIdAsync(VersionUpload.VersionId.Value);
                    Plugin = await client.Plugins.GetPluginByIdAsync(Version.PluginId);                    
                }
            } catch (ShearlegsWebAPIRequestException exception)
            {
                loggingBroker.LogException(exception);
            }

            BreadcrumbItems.Add(new BreadcrumbItem($"#{VersionUpload.Id}", null, true));
            isLoaded = true;
        }

        private async Task MigrateToVersionAsync()
        {
            try
            {
                Version version = await client.VersionUploads.MigrateVersionUploadToVersionAsync(VersionUpload.Id);

                Plugin plugin = await client.Plugins.GetPluginByIdAsync(version.PluginId);
                snackbar.Add($"Successfully created a new version {plugin.PackageId} {version.Name}", Severity.Success);

            } catch (ShearlegsWebAPIRequestException exception)
            {
                snackbar.Add($"Error occured when trying to migrate to version", Severity.Error);
                loggingBroker.LogException(exception);
            }
        }
    }
}
    