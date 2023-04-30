﻿using MudBlazor;
using Shearlegs.Web.APIClient.Models.Nodes;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Nodes
{
    public partial class NodesManagementPage
    {
        public List<BreadcrumbItem> BreadcrumbItems = new()
        {
            new BreadcrumbItem("Home", "/"),
            new BreadcrumbItem("Management", "/management"),
            new BreadcrumbItem("Nodes", null, true)
        };
        
        public List<Node> Nodes { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Nodes = await client.Nodes.GetAllNodesAsync();
        }

        private string searchString = string.Empty;

        private bool SearchNode(Node node)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (node.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (node.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (node.FQDN.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (node.UpdateUser?.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                return true;
            if (node.CreateUser?.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                return true;
            return false;
        }
    }
}