using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
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

        public MResult Result { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Result = await ResultsRepository.GetResultAsync(ResultId);
        }
    }
}
