using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Admin.PluginsPage.Components
{
    public partial class PluginModal
    {
        public override string ModalId => nameof(PluginModal);

        [Inject]
        public PluginsRepository PluginsRepository { get; set; }
        [Inject]
        public UserService UserService { get; set; }

        [Parameter]
        public EventCallback<MPlugin> OnPluginCreated { get; set; }

        public MPlugin Model { get; set; } = new MPlugin();


        private bool isLoading = false;
        public async Task SubmitAsync()
        {
            isLoading = true;

            Model.CreateUserId = UserService.UserId;
            Model.UpdateUserId = UserService.UserId;
            MPlugin plugin = await PluginsRepository.AddPluginAsync(Model);
            
            isLoading = false;

            await HideAsync();
            await OnPluginCreated.InvokeAsync(plugin);
            
            Model = new MPlugin();
        }

    }
}
