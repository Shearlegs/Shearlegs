using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Users;
using Shearlegs.Web.APIClient.Models.Users.Requests;
using Shearlegs.Web.Dashboard.Models.Forms.Managements.Users;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Users
{
    public partial class UserManagementPage
    {
        [Parameter]
        public string Username { get; set; }

        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Management", "/management"),
            new BreadcrumbItem("Users", "/management/users")
        };

        public User User { get; set; }
        public UpdateUserFormModel Model { get; set; } = new();

        private bool isLoaded = false;
        private bool isCanceled = false;
        private bool isUpdateUserProcessing = false;
        private bool showSuccessAlert = false;
        private bool showErrorAlert = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                User = await client.Users.GetUserByNameAsync(Username);
            } catch (ShearlegsWebAPIRequestException) 
            {
                isCanceled = true;
                BreadcrumbItems.Add(new BreadcrumbItem("Not Found", null, true));
                return;
            }            

            BreadcrumbItems.Add(new BreadcrumbItem(User.Name, null, true));
            Model = new()
            {                
                Role = User.Role,
                Password = null
            };            

            isLoaded = true;
        }

        private async Task HandleUpdateUserAsync()
        {
            isUpdateUserProcessing = true;
            showSuccessAlert = false;
            showErrorAlert = false;

            ModifyUserIdentityRequest request = new()
            {
                Role = Model.Role,
                Password = Model.Password
            };

            User = await client.Users.ModifyUserIdentityAsync(User.Id, request);

            Model = new()
            {
                Role = User.Role,
                Password = null
            };

            showSuccessAlert = true;
            isUpdateUserProcessing = false;
        }
    }
}
