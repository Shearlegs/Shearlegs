using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Admin.PluginPage.Components
{
    public partial class SecretsTable
    {
        [Parameter]
        public MPlugin Plugin { get; set; }

        [Inject]
        public PluginsRepository PluginsRepository { get; set; }

        [Inject]
        public UserService UserService { get; set; }

        public List<MPluginSecret> Secrets => Plugin.Secrets;

        public MPluginSecret AddedSecret { get; set; }
        public MPluginSecret DeletedSecret { get; set; }

        public MPluginSecret SecretModel { get; set; } = new MPluginSecret();
        private bool isInvalid = false;
        private bool isDuplicate = false;
        private bool isSuccess = false;
        private bool isDeleted = false;

        private void HideFlags()
        {
            isInvalid = false;
            isDuplicate = false;
            isSuccess = false;
            isDeleted = false;

        }

        public async Task AddSecretAsync()
        {
            HideFlags();

            if (string.IsNullOrEmpty(SecretModel.Name) || string.IsNullOrEmpty(SecretModel.Value))
            {
                isInvalid = true;
                return;
            }


            if (Plugin.Secrets.Exists(x => x.Name.Equals(SecretModel.Name, StringComparison.OrdinalIgnoreCase)))
            {
                isDuplicate = true;
                return;
            }

            SecretModel.PluginId = Plugin.Id;
            SecretModel.CreateUserId = UserService.UserId;
            AddedSecret = await PluginsRepository.AddPluginSecretAsync(SecretModel);
            Plugin.Secrets.Add(AddedSecret);
            SecretModel = new MPluginSecret();

            isSuccess = true;
        }

        public async Task DeleteSecretAsync(MPluginSecret secret)
        {
            HideFlags();

            await PluginsRepository.DeletePluginSecretAsync(secret.Id);
            Plugin.Secrets.Remove(secret);

            DeletedSecret = secret;
            isDeleted = true;
        }
    }
}
