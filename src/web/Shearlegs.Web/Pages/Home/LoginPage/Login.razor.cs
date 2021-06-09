using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Models.Params;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Home.LoginPage
{
    public partial class Login
    {
        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public UsersRepository UsersRepository { get; set; }

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

        protected override async Task OnInitializedAsync()
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
