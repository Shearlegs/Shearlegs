using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Constants;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Pages.Admin.Users.Components;
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
            User = await UsersRepository.UpdateUserAsync(Model);
            Model = User.MakeCopy();
            await Task.Delay(1000); 
            isLoading = false;
            isUpdated = true;
        }
    }
}
