using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
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

        public IEnumerable<MPlugin> Plugins { get; set; }

        private string searchString;

        public List<MPlugin> SearchedPlugins
            => Plugins.Where(x => string.IsNullOrEmpty(searchString) || x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(x => x.UpdateDate).ToList();

        protected override async Task OnInitializedAsync()
        {
            Plugins = await PluginsRepository.GetPluginsAsync();
        }


    }
}
