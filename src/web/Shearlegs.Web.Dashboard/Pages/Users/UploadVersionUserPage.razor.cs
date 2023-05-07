using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.Dashboard.Pages.Users
{
    public partial class UploadVersionUserPage
    {
        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("User", "/User"),
            new BreadcrumbItem("Upload Version", null, true)
        };

        public IBrowserFile VersionFile { get; set; }
        public bool HasFileSelected => VersionFile != null;
        private bool isUploadProcessing = false;

        private async Task OnVersionFileChanged(InputFileChangeEventArgs args)
        {
            VersionFile = args.File;

            await InvokeAsync(StateHasChanged);
        }

        private async Task UploadAndTestAsync()
        {
            isUploadProcessing = true;
            StateHasChanged();
        }
    }
}
