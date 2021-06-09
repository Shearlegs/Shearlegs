using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Home.IndexPage
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpContextAccessor Accessor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!Accessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                NavigationManager.NavigateTo("/login");
            }
        }
    }
}
