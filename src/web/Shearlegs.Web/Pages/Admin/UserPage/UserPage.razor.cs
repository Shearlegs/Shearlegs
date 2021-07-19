using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Constants;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Pages.Admin.Users.Components;
using Shearlegs.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Admin.UserPage
{
    [Authorize(Roles = RoleConstants.AdminRoleId)]
    public partial class UserPage
    {
        [Parameter]
        public int UserId { get; set; }

        [Inject]
        public UsersRepository UsersRepository { get; set; }
        [Inject]
        public UserService UserService { get; set; }

        public MUser User { get; set; }

        public MUser Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            User = await UsersRepository.GetUserAsync(UserId);
            Model = User.MakeCopy();
        }

        private bool isUpdated = false;
        private bool isLoading = false;
        public async Task UpdateUserAsync()
        {
            isLoading = true;
            List<MUserPlugin> plugins = User.Plugins;
            User = await UsersRepository.UpdateUserAsync(Model);
            User.Plugins = plugins;
            Model = User.MakeCopy();
            isLoading = false;
            isUpdated = true;
        }
    }
}
