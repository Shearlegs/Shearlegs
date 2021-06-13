﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shearlegs.API.Plugins;
using Shearlegs.API.Plugins.Info;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Developer.UploadPage
{
    public partial class UploadPage
    {
        [Inject]
        public IPluginManager PluginManager { get; set; }
        [Inject]
        public PluginsRepository PluginsRepository { get; set; }
        [Inject]
        public VersionsRepository VersionsRepository { get; set; }
        [Inject]
        public UserService UserService { get; set; }

        private byte[] pluginData;
        public IPluginInfo PluginInfo { get; set; }
        public MPlugin Plugin { get; set; }

        private bool isLoading = false;
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            if (e.FileCount == 0)
                return;

            isLoading = true;

            pluginData = new byte[e.File.Size];
            await e.File.OpenReadStream(30 * 1024 * 1024).ReadAsync(pluginData);

            PluginInfo = await PluginManager.GetPluginInfoAsync(pluginData);
            if (PluginInfo != null)
            {
                Plugin = await PluginsRepository.GetPluginAsync(PluginInfo.PackageId);
            }

            isLoading = false;
        }

        private bool isLoading2 = false;
        public async Task UploadVersionAsync()
        {
            isLoading2 = true;
            MVersion version = new MVersion()
            {
                PluginId = Plugin.Id,
                Name = PluginInfo.Version,
                PackageContent = pluginData,
                CreateUserId = UserService.UserId,
                Parameters = new List<MVersionParameter>()
            };

            foreach (IPluginParameterInfo parameterInfo in PluginInfo.Parameters)
            {
                version.Parameters.Add(MVersionParameter.FromParameterInfo(parameterInfo));
            }

            await VersionsRepository.AddVersionAsync(version);
            isLoading2 = false;
        }

    }
}