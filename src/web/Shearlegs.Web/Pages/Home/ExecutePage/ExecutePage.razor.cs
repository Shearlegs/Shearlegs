using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Extensions;
using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Home.ExecutePage
{
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
            VersionId = (int)args.Value;
            Version = await VersionsRepository.GetVersionAsync(VersionId);
        }

        public async Task SubmitAsync()
        {
            string json = await JSRuntime.GetFormDataJsonAsync("parameters");

            Console.WriteLine(json);
        }
    }
}
