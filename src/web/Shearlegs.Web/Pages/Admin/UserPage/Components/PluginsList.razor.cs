using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Admin.UserPage.Components
{
    public partial class PluginsList
    {
        [Parameter]
        public MUser User { get; set; }

        [Inject]
        public PluginsRepository PluginsRepository { get; set; }
        [Inject]
        public UserService UserService { get; set; }
        [Inject]
        public UsersRepository UsersRepository { get; set; }

        public IEnumerable<MPlugin> Plugins { get; set; }

        public IEnumerable<MPlugin> FilteredPlugins => Plugins.Where(x => !User.Plugins.Exists(y => y.PluginId == x.Id));

        protected override async Task OnInitializedAsync()
        {
            Plugins = await PluginsRepository.GetPluginsAsync();
        }

        public string packageId;

        private bool isNotFound = false;
        private bool isAlreadyExists = false;
        private bool isSuccess = false;
        private bool isDeleted = false;

        public MUserPlugin AddedUserPlugin { get; set; }
        public MUserPlugin DeletedUserPlugin { get; set; }

        public void HideFlags()
        {
            isNotFound = false;
            isAlreadyExists = false;
            isSuccess = false;
            isDeleted = false;
        }

        public async Task AddUserPluginAsync()
        {
            HideFlags();

            MPlugin plugin = Plugins.FirstOrDefault(x => x.PackageId.Equals(packageId, StringComparison.OrdinalIgnoreCase));
            if (plugin == null)
            {
                isNotFound = true;
                return;
            }

            if (User.Plugins.Exists(x => x.PluginId == plugin.Id))
            {
                isAlreadyExists = true;
                return;
            }

            MUserPlugin userPlugin = new MUserPlugin()
            {
                PluginId = plugin.Id,
                UserId = User.Id,
                CreateUserId = UserService.UserId
            };

            AddedUserPlugin = await UsersRepository.AddUserPluginAsync(userPlugin);
            User.Plugins.Add(AddedUserPlugin);

            packageId = string.Empty;

            isSuccess = true;
        }

        public async Task DeleteUserPluginAsync(MUserPlugin userPlugin)
        {
            HideFlags();

            await UsersRepository.DeleteUserPluginAsync(userPlugin.Id);
            User.Plugins.Remove(userPlugin);
            DeletedUserPlugin = userPlugin;
            isDeleted = true;

        }
    }
}
