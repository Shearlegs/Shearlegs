using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shearlegs.Web.Constants;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Extensions;
using Shearlegs.Web.Models;
using Shearlegs.Web.Services;
using Shearlegs.Web.Shared.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Admin.Users.Components
{
    public partial class UserModal
    {
        [Inject]
        public UsersRepository UsersRepository { get; set; }
        [Inject]
        public UserService UserService { get; set; }

        [Parameter]
        public EventCallback<MUser> OnUserCreated { get; set; }

        public MUser Model { get; set; } = new MUser() { Role = RoleConstants.GuestRoleId };

        public override string ModalId => nameof(UserModal);

        private bool isLoading = false;
        public async Task SubmitAsync()
        {
            isLoading = true;
            MUser user = await UsersRepository.AddUserAsync(Model);
            isLoading = false;
            await HideAsync();
            await OnUserCreated.InvokeAsync(user);
            Model = new MUser();
        }
    }
}
