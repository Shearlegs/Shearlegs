using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
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

        private bool isLoaded = false;
        private bool isCanceled = false;

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                VersionUpload = await client.VersionUploads.GetVersionUploadByIdAsync(VersionUploadId);
            } catch (ShearlegsWebAPIRequestException)
            {
                isCanceled = true;
                BreadcrumbItems.Add(new BreadcrumbItem("Not Found", null, true));
                return;
            }

            BreadcrumbItems.Add(new BreadcrumbItem($"#{VersionUpload.Id}", null, true));
            isLoaded = true;
        }
    }
}
