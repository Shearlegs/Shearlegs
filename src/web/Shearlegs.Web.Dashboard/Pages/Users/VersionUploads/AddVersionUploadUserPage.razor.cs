using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.VersionUploads;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Shearlegs.Web.Dashboard.Pages.Users.VersionUploads
{
    public partial class AddVersionUploadUserPage
    {
        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("User", "/user"),
            new BreadcrumbItem("Version Uploads", "/user/versionuploads"),
            new BreadcrumbItem("Add", null, true)
        };

        public IBrowserFile VersionFile { get; set; }
        public string VersionContentType { get; set; }
        public bool HasFileSelected => VersionFile != null;
        private bool isUploadProcessing = false;

        private async Task OnVersionFileChanged(InputFileChangeEventArgs args)
        {
            if (args.File == null)
            {
                VersionFile = null;
                VersionContentType = null;
            } else
            {
                VersionFile = args.File;
                if (string.IsNullOrEmpty(VersionFile.ContentType)) 
                {
                    VersionContentType = MimeTypes.GetMimeType(VersionFile.Name);               
                } else
                {
                    VersionContentType = VersionFile.ContentType;
                }
            }            

            await InvokeAsync(StateHasChanged);
        }

        private async Task UploadAndTestAsync()
        {
            isUploadProcessing = true;

            using MemoryStream memoryStream = new();
            using (Stream stream = VersionFile.OpenReadStream()) 
            {
                await stream.CopyToAsync(memoryStream);
            }
            string name = "formFile";
            string fileName = VersionFile.Name;

            IFormFile formFile = new FormFile(memoryStream, 0, memoryStream.Length, name, fileName) 
            {
                Headers = new HeaderDictionary(),
                ContentType = VersionContentType                
            };

            VersionUpload versionUpload = await client.VersionUploads.AddVersionUploadAsync(formFile);
            navigationManager.NavigateTo($"/user/versionuploads/{versionUpload.Id}");

            isUploadProcessing = false;
            StateHasChanged();
        }
    }
}
