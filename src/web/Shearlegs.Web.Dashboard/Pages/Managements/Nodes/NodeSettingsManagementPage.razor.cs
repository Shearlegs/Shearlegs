using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Nodes;
using Shearlegs.Web.APIClient.Models.Nodes.Requests;
using Shearlegs.Web.Dashboard.Models.Forms.Managements.Nodes;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Nodes
{
    public partial class NodeSettingsManagementPage
    {
        [Parameter]
        public string NodeName { get; set; }

        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Management", "/management"),
            new BreadcrumbItem("Nodes", "/management/nodes")
        };

        public Node Node { get; set; }
        public UpdateNodeFormModel Model { get; set; }

        private bool isLoaded = false;
        private bool isCanceled = false;
        private bool isUpdateUserProcessing = false;
        private bool showSuccessAlert = false;
        private bool showErrorAlert = false;

        protected override async Task OnParametersSetAsync()
        {
            while (BreadcrumbItems.Count > 3)
            {
                BreadcrumbItems.RemoveAt(3);
            }

            try
            {
                Node = await client.Nodes.GetNodeByNameAsync(NodeName);
            }
            catch (ShearlegsWebAPIRequestException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
            {
                isCanceled = true;
                BreadcrumbItems.Add(new BreadcrumbItem("Not Found", null, true));
                return;
            }

            BreadcrumbItems.Add(new BreadcrumbItem(Node.Name, $"/management/nodes/{Node.Name}"));
            BreadcrumbItems.Add(new BreadcrumbItem("Settings", null, true));

            Model = new()
            {
                Name = Node.Name,
                Description = Node.Description,
                FQDN = Node.FQDN,
                Scheme = Node.Scheme,
                HttpPort = Node.HttpPort,
                HttpsPort = Node.HttpsPort,
                CacheSizeLimit = Node.CacheSizeLimit,
                IsBehindProxy = Node.IsBehindProxy,
                IsEnabled = Node.IsEnabled
            };

            isLoaded = true;
        }

        private async Task HandleUpdateNodeAsync()
        {
            isUpdateUserProcessing = true;
            showSuccessAlert = false;
            showErrorAlert = false;

            UpdateNodeRequest request = new()
            {
                Name = Model.Name,
                Description = Model.Description,
                FQDN = Model.FQDN,
                Scheme = Model.Scheme,
                HttpPort = Model.HttpPort,
                HttpsPort = Model.HttpsPort,
                CacheSizeLimit = Model.CacheSizeLimit,
                IsBehindProxy = Model.IsBehindProxy,
                IsEnabled = Model.IsEnabled
            };

            try
            {
                Node = await client.Nodes.UpdateNodeAsync(Node.Id, request);

                if (Node.Name != NodeName)
                {
                    navigationManager.NavigateTo($"/management/nodes/{Node.Name}");
                }

                showSuccessAlert = true;
            }
            catch (ShearlegsWebAPIRequestException exception)
            {
                loggingBroker.LogException(exception);
                showErrorAlert = true;
            }

            isUpdateUserProcessing = false;
        }
    }
}
