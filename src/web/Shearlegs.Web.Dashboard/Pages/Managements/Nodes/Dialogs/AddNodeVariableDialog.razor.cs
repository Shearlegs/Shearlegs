using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Nodes;
using Shearlegs.Web.APIClient.Models.NodeVariables;
using Shearlegs.Web.APIClient.Models.NodeVariables.Requests;
using Shearlegs.Web.Dashboard.Models.Forms.Managements.NodeVariables;
using System.Threading.Tasks;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Nodes.Dialogs
{
    public partial class AddNodeVariableDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public Node Node { get; set; }

        public EditForm EditForm { get; set; }
        public AddNodeVariableFormModel Model { get; set; } = new();

        private bool isProcessing = false;
        private bool showErrorAlert = false;

        public async Task SubmitAsync()
        {
            isProcessing = true;
            showErrorAlert = false;

            try
            {
                var request = new AddNodeVariableRequest
                {
                    Name = Model.Name,
                    Value = Model.Value,
                    Description = Model.Description,
                    IsSensitive = Model.IsSensitive
                };

                NodeVariable nodeVariable = await client.Nodes.AddNodeVariableAsync(Node.Id, request);
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
