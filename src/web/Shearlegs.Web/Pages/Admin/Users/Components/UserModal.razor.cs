using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Extensions;
using Shearlegs.Web.Models;
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
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public EventCallback<User> OnUserCreated { get; set; }

        public User Model { get; set; } = new User();

        public string ModalId => nameof(UserModal);

        public async Task ShowAsync()
        {
            await JSRuntime.ShowModalStaticAsync(ModalId);
        }

        public async Task HideAsync()
        {
            await JSRuntime.HideModalAsync(ModalId);
        }

        private bool isLoading = false;
        public async Task SubmitAsync()
        {
            isLoading = true;
            User user = await UsersRepository.AddUserAsync(Model);
            isLoading = false;
            await OnUserCreated.InvokeAsync(user);
            Model = new User();
        }
    }
}
