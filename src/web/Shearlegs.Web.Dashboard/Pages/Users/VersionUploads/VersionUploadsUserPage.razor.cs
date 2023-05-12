using MudBlazor;
using Shearlegs.Web.APIClient.Models.VersionUploads;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.Dashboard.Pages.Users.VersionUploads
{
    public partial class VersionUploadsUserPage
    {
        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("User", "/user"),
            new BreadcrumbItem("Version Uploads", null)
        };

        public List<VersionUpload> VersionUploads { get; set; }

        protected override async Task OnInitializedAsync()
        {
            VersionUploads = await client.VersionUploads.GetVersionUploadsByUserIdAsync(userState.User.Id);
        }

        private string searchString = string.Empty;

        private bool SearchVersionUpload(VersionUpload versionUpload)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (versionUpload.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (versionUpload.FileName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (versionUpload.Status.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (versionUpload.PackageId != null && versionUpload.PackageId.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (versionUpload.PackageVersion != null && versionUpload.PackageVersion.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }
}
