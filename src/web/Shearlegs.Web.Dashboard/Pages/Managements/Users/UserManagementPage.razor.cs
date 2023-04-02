using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Users;
using Shearlegs.Web.APIClient.Models.Users.Requests;
using Shearlegs.Web.Dashboard.Models.Forms;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Users
{
    public partial class UserManagementPage
    {
        [Parameter]
        public int UserId { get; set; }

        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Management", "/management"),
            new BreadcrumbItem("Users", "/management/users")
        };

        public User User { get; set; }
        public UpdateUserFormModel Model { get; set; } = new();

        private bool isLoaded = false;
        private bool isUpdateUserProcessing = false;
        private bool showSuccessAlert = false;
        private bool showErrorAlert = false;

        protected override async Task OnInitializedAsync()
        {
            User = await client.Users.GetUserByIdAsync(UserId);

            BreadcrumbItems.Add(new BreadcrumbItem(User.Name, null, true));
            Model = new()
            {
                Username = User.Name,
                Role = User.Role,
                Password = null
            };

            await Task.Delay(1000);
            

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
                Username = User.Name,
                Role = User.Role,
                Password = null
            };

            showSuccessAlert = true;
            isUpdateUserProcessing = false;
        }
    }
}
