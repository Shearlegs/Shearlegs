using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Nodes;
using System.Net;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Nodes
{
    public partial class NodeVariablesManagementPage
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

        private bool isLoaded = false;
        private bool isCanceled = false;

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
            BreadcrumbItems.Add(new BreadcrumbItem("Variables", null, true));

            isLoaded = true;
        }
    }
}
