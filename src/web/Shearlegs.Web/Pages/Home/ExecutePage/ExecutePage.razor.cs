using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Result;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Extensions;
using Shearlegs.Web.Models;
using Shearlegs.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Home.ExecutePage
{
    [Authorize]
    public partial class ExecutePage
    {
        [Parameter]
        public int PluginId { get; set; }

        [Inject]
        public PluginsRepository PluginsRepository { get; set; }
        [Inject]
        public VersionsRepository VersionsRepository { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public PluginService PluginService { get; set; }
        [Inject]
        public UserService UserService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public MPlugin Plugin { get; set; }

        public int VersionId { get; set; }
        public MVersion Version { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Plugin = await PluginsRepository.GetPluginAsync(PluginId);
            VersionId = Plugin.Versions.OrderByDescending(x => x.CreateDate).FirstOrDefault()?.Id ?? 0;

            if (VersionId != 0)
            {
                Version = await VersionsRepository.GetVersionAsync(VersionId);
            }
        }

        public async Task OnChangeVersionAsync(ChangeEventArgs args)
        {
            VersionId = Convert.ToInt32(args.Value.ToString());
            Version = await VersionsRepository.GetVersionAsync(VersionId);
        }

        public IPluginResult Result { get; set; }
        private bool isExecuting = false;
        public async Task SubmitAsync()
        {
            string json = await JSRuntime.GetFormDataJsonAsync("parameters");
            isExecuting = true;
            StateHasChanged();
            int resultId = await PluginService.ExecuteVersionAsync(UserService.UserId, Version.Id, json);
            isExecuting = false;
            NavigationManager.NavigateTo($"/results/{resultId}");
            
        }
    }
}
