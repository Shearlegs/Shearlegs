using MudBlazor;
using Shearlegs.Web.APIClient.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Users
{
    public partial class UsersManagementPage
    {
        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Management", "/management"),
            new BreadcrumbItem("Users", null, true)
        };

        public List<User> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Users = await client.Users.GetAllUsersAsync();
        }

        private string searchString = string.Empty;

        private bool SearchUser(User user)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (user.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (user.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (user.Role.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}
