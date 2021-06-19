using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Shearlegs.Web.Constants;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Admin.PluginPage
{
    [Authorize(Roles = RoleConstants.AdminRoleId)]
    public partial class PluginPage
    {
        [Parameter]
        public int PluginId { get; set; }

        [Inject]
        public PluginsRepository PluginsRepository { get; set; }
        [Inject]
        public UserService UserService { get; set; }

        public List<MVersion> Versions => Plugin.Versions.OrderByDescending(x => x.CreateDate).ToList();
        public List<MPluginSecret> Secrets => Plugin.Secrets;

        public MPlugin Plugin { get; set; }
        public MPlugin Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Plugin = await PluginsRepository.GetPluginAsync(PluginId);
            Model = Plugin.MakeCopy();
        }

        private bool isUpdated = false;
        private bool isLoading = false;

        public async Task UpdatePluginAsync()
        {
            isLoading = true;
            Model.UpdateUserId = UserService.UserId;
            await PluginsRepository.UpdatePluginAsync(Model);
            isLoading = false;
            isUpdated = true;
        }

    }
}
