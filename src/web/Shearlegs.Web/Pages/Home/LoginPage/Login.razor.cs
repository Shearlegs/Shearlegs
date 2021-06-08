using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shearlegs.Web.Models.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Home.LoginPage
{
    public partial class Login
    {
        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public LoginParams LoginParams { get; set; } = new LoginParams();

        protected override async Task OnInitializedAsync()
        {
        }

        private async Task LoginAsync()
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/signin", LoginParams);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
