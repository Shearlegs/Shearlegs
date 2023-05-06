using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public Dictionary<int, NodeDaemonInfo> NodeDaemons { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Nodes = await client.Nodes.GetAllNodesAsync();

            foreach (Node node in Nodes)
            {
                _ = RefreshNodeDaemon(node.Id);
            }
        }

        private async Task RefreshNodeDaemon(int nodeId)
        {
            NodeDaemonInfo daemonInfo;

            try
            {
                daemonInfo = await client.Nodes.GetNodeDaemonInfoAsync(nodeId);
            } catch (ShearlegsWebAPIRequestException)
            {
                daemonInfo = null;
            }

            if (NodeDaemons.ContainsKey(nodeId))
            {
                NodeDaemons[nodeId] = daemonInfo;
            } else
            {
                NodeDaemons.Add(nodeId, daemonInfo);
            }

            await InvokeAsync(StateHasChanged);
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
