using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Nodes;
using Shearlegs.Web.APIClient.Models.NodeVariables;
using Shearlegs.Web.Dashboard.Pages.Managements.Nodes.Dialogs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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
        public List<NodeVariable> NodeVariables { get; set; }

        private bool isLoaded = false;
        private bool isCanceled = false;
        private bool isVariablesLoading = true;
        private string searchString = string.Empty;

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

            await Task.Factory.StartNew(LoadVariables);
        }

        private async Task LoadVariables()
        {
            isVariablesLoading = true;
            NodeVariables = await client.Nodes.GetNodeVariablesAsync(Node.Id);
            isVariablesLoading = false;
            await InvokeAsync(StateHasChanged);
        }

        private bool SearchNodeVariable(NodeVariable nodeVariable)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (nodeVariable.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (nodeVariable.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (nodeVariable.Description.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (nodeVariable.UpdateUser?.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                return true;
            if (nodeVariable.CreateUser?.Name.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ?? false)
                return true;
            return false;
        }

        private async Task OpenAddDialogAsync()
        {
            DialogOptions options = new() 
            { 
                CloseOnEscapeKey = true,
                DisableBackdropClick = true,
                FullWidth = true,
                Position = DialogPosition.TopCenter
            };
            DialogParameters parameters = new()
            {
                { "Node", Node }
            };

            IDialogReference dialog = dialogService.Show<AddNodeVariableDialog>(null, parameters, options);

            DialogResult result = await dialog.Result;

            if (!result.Cancelled)
            {
                NodeVariable nodeVariable = result.Data as NodeVariable;
                NodeVariables.Add(nodeVariable);
                StateHasChanged();
            }
        }

        private async Task OpenEditDialogAsync(NodeVariable variable)
        {
            DialogOptions options = new()
            {
                CloseOnEscapeKey = true,
                DisableBackdropClick = true,
                FullWidth = true,
                Position = DialogPosition.TopCenter
            };
            DialogParameters parameters = new()
            {
                { "Node", Node },
                { "NodeVariable", variable }
            };

            IDialogReference dialog = dialogService.Show<EditNodeVariableDialog>(null, parameters, options);

            DialogResult result = await dialog.Result;

            if (!result.Cancelled)
            {
                NodeVariable nodeVariable = result.Data as NodeVariable;
                NodeVariables.RemoveAll(x => x.Id == nodeVariable.Id);
                NodeVariables.Add(nodeVariable);
                StateHasChanged();
            }
        }
    }
}
