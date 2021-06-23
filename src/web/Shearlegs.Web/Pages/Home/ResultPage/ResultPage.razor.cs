using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Constants;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using Shearlegs.Web.Services;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Home.ResultPage
{
    [Authorize]
    public partial class ResultPage
    {
        [Parameter]
        public int ResultId { get; set; }

        [Inject]
        public ResultsRepository ResultsRepository { get; set; }
        [Inject]
        public UserService UserService { get; set; }

        public MResult Result { get; set; }

        private bool hasPermission = true;

        protected override async Task OnParametersSetAsync()
        {
            if (!UserService.IsInRole(RoleConstants.AdminRoleId))
            {
                hasPermission = await ResultsRepository.IsResultUserAsync(ResultId, UserService.UserId);
                if (!hasPermission)
                {
                    return;
                }
            }

            Result = await ResultsRepository.GetResultAsync(ResultId);
        }
    }
}
