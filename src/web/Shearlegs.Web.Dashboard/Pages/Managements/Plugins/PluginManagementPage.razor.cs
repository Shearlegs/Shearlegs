﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Plugins;
using System.Net;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Plugins
{
    public partial class PluginManagementPage
    {
        [Parameter]
        public string PackageId { get; set; }

        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Management", "/management"),
            new BreadcrumbItem("Plugins", "/management/plugins")
        };

        public Plugin Plugin { get; set; }

        private bool isLoaded = false;
        private bool isCanceled = false;

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Plugin = await client.Plugins.GetPluginByPackageIdAsync(PackageId);
            } catch (ShearlegsWebAPIRequestException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
            {
                isCanceled = true;
                BreadcrumbItems.Add(new BreadcrumbItem("Not Found", null, true));
                return;
            }

            BreadcrumbItems.Add(new BreadcrumbItem(Plugin.PackageId, null, true));

            isLoaded = true;
        }
    }
}
