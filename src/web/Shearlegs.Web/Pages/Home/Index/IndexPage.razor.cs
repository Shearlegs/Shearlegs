using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Home.Index
{
    public partial class IndexPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public UserService UserService { get; set; }
        [Inject]
        public PluginsRepository PluginsRepository { get; set; }


        private string searchString;

        public List<MPlugin> SearchedPlugins
            => Plugins.Where(x => string.IsNullOrEmpty(searchString)
                || x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                ).OrderByDescending(x => x.UpdateDate).ToList();

        public IEnumerable<MPlugin> Plugins { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!UserService.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/login");
            }

            Plugins = await PluginsRepository.GetPluginsAsync();
        }

        private void GoToPlugin(MPlugin plugin)
        {
            NavigationManager.NavigateTo($"/execute/{plugin.Id}");
        }
    }
}
