using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shearlegs.Web.APIClient.Models.Exceptions;
using Shearlegs.Web.APIClient.Models.Nodes;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Shearlegs.Web.Dashboard.Pages.Managements.Nodes
{
    public partial class NodeManagementPage
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
        public NodeDaemonInfo DaemonInfo { get; set; }
        public NodeDaemonStatistics DaemonStatistics { get; set; }

        private bool isLoaded = false;
        private bool isCanceled = false;
        private bool isOffline = false;
        private ShearlegsWebAPIRequestException daemonException;

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

            BreadcrumbItems.Add(new BreadcrumbItem(Node.Name, null, true));

            _ = RefreshDaemonInfoAsync();
            _ = RefreshDaemonStatisticsAsync();

            isLoaded = true;
        }

        private async Task RefreshDaemonInfoAsync()
        {
            try
            {
                DaemonInfo = await client.Nodes.GetNodeDaemonInfoAsync(Node.Id);
            } catch (ShearlegsWebAPIRequestException exception) when (exception.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                isOffline = true;
                daemonException = exception;
            } catch (ShearlegsWebAPIRequestException exception)
            {
                loggingBroker.LogException(exception);
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task RefreshDaemonStatisticsAsync()
        {
            try
            {
                DaemonStatistics = await client.Nodes.GetNodeDaemonStatisticsAsync(Node.Id);
            }  catch (ShearlegsWebAPIRequestException exception) when (exception.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                isOffline = true;
                daemonException = exception;
            } catch (ShearlegsWebAPIRequestException exception)
            {
                loggingBroker.LogException(exception);
            }

            await InvokeAsync(StateHasChanged);
        }
    }
}
