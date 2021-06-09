using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Shearlegs.Web.Models.Params;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Home.Login
{
    public partial class LoginPage
    {
        [Inject]
        public HttpClient HttpClient { get; set; }  

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpContextAccessor Accessor { get; set; }

        public LoginParams LoginParams { get; set; } = new LoginParams();

        public bool IsWrong { get; set; }

        public bool IsInvalid 
        {
            get 
            {
                return string.IsNullOrEmpty(LoginParams.Name) || string.IsNullOrEmpty(LoginParams.Password);
            } 
        }

        protected override void OnInitialized()
        {
            if (Accessor.HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                NavigationManager.NavigateTo("/");
            }

            Uri uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("isWrong", out StringValues isWrong))
            {
                IsWrong = Convert.ToBoolean(isWrong);
            }
        }
    }
}
