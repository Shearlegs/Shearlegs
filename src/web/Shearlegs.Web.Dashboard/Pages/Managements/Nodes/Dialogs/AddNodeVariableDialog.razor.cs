using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Nodes;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Nodes.Dialogs
{
    public partial class AddNodeVariableDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public Node Node { get; set; }

        void Submit()
        {
            MudDialog.Close(DialogResult.Ok(true));
        }

        void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
