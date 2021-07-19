using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public UserService UserService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!UserService.HasUserId)
            {
                NavigationManager.NavigateTo("/login");
            }
        }
    }
}
