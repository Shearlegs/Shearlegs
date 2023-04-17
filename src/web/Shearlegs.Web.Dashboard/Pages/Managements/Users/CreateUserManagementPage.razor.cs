using Microsoft.AspNetCore.Http.HttpResults;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Users;
using Shearlegs.Web.APIClient.Models.Users.Requests;
using Shearlegs.Web.Dashboard.Models.Forms.Managements.Users;
using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Users
{
    public partial class CreateUserManagementPage
    {
        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Management", "/management"),
            new BreadcrumbItem("Users", "/management/users"),
            new BreadcrumbItem("Create", null, true)
        };

        public CreateUserFormModel Model { get; set; } = new()
        {
            Role = UserRole.Guest
        };

        private bool isCreateUserProcessing = false;
        private bool showSuccessAlert = false;
        private bool showErrorAlert = false;

        private async Task HandleCreateUserAsync()
        {
            isCreateUserProcessing = true;
            showSuccessAlert = false;
            showErrorAlert = false;

            CreateUserRequest request = new()
            {
                Name = Model.Name,
                Role = Model.Role,                
                PasswordText = Model.Password,
                AuthenticationType = UserAuthenticationType.Password
            };

            try
            {
                User user = await client.Users.CreateUserAsync(request);
                showSuccessAlert = true;

                navigationManager.NavigateTo($"/management/users/{user.Id}");                
            } catch (ShearlegsWebAPIRequestException)
            {
                showErrorAlert = true;
            }

            isCreateUserProcessing = false;
        }
    }
}
