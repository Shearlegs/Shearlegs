using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Pages.Admin.PluginsPage.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Admin.PluginsPage
{
    public partial class PluginsPage
    {
        [Inject]
        public PluginsRepository PluginsRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<MPlugin> Plugins { get; set; }

        

        private string searchString;

        public List<MPlugin> SearchedPlugins
            => Plugins.Where(x => string.IsNullOrEmpty(searchString) 
                || x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                || x.PackageId.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                )
                .OrderByDescending(x => x.UpdateDate).ToList();

        protected override async Task OnInitializedAsync()
        {
            Plugins = await PluginsRepository.GetPluginsAsync();
        }

        public PluginModal Modal { get; set; }

        public async Task ShowModalAsync()
        {
            await Modal.ShowAsync();
        }

        public void OnPluginCreated(MPlugin plugin)
        {
            NavigationManager.NavigateTo($"/admin/plugins/{plugin.Id}");
        }
    }
}
