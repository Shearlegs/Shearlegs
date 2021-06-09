using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace Shearlegs.Web.Pages.Home.Index
{
    public partial class IndexPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpContextAccessor Accessor { get; set; }

        protected override void OnInitialized()
        {
            if (!Accessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                NavigationManager.NavigateTo("/login");
            }
        }
    }
}
