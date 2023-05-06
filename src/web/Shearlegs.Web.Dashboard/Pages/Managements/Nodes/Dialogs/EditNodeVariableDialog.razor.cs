using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Nodes;
using Shearlegs.Web.APIClient.Models.NodeVariables.Requests;
using Shearlegs.Web.APIClient.Models.NodeVariables;
using Shearlegs.Web.Dashboard.Models.Forms.Managements.NodeVariables;
using System.Threading.Tasks;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Nodes.Dialogs
{
    public partial class EditNodeVariableDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public Node Node { get; set; }
        [Parameter]
        public NodeVariable NodeVariable { get; set; }

        public UpdateNodeVariableFormModel Model { get; set; } = new();

        private bool isProcessing = false;
        private bool showErrorAlert = false;

        protected override void OnParametersSet()
        {
            Model = new()
            {
                Name = NodeVariable.Name,
                Value = NodeVariable.Value,
                Description = NodeVariable.Description,
                IsSensitive = NodeVariable.IsSensitive
            };
        }

        public async Task SubmitAsync()
        {
            isProcessing = true;
            showErrorAlert = false;

            try
            {
                var request = new UpdateNodeVariableRequest
                {
                    Name = Model.Name,
                    Value = Model.Value,
                    Description = Model.Description,
                    IsSensitive = Model.IsSensitive
                };

                NodeVariable nodeVariable = await client.NodeVariables.UpdateNodeVariableAsync(NodeVariable.Id, request);
                MudDialog.Close(DialogResult.Ok(nodeVariable));
            }
            catch (ShearlegsWebAPIRequestException exception)
            {
                showErrorAlert = true;
                loggingBroker.LogException(exception);
            }

            isProcessing = false;
        }

        void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
