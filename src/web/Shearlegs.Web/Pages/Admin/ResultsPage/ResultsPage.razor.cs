using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Constants;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Admin.ResultsPage
{
    [Authorize(Roles = RoleConstants.AdminRoleId)]
    public partial class ResultsPage
    {
        [Inject]
        public ResultsRepository ResultsRepository { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<MResult> Results { get; set; }

        public List<MResult> SearchedResults 
            => Results.Where(x => string.IsNullOrEmpty(searchString)
                || x.Id.ToString().Equals(searchString)
                || x.User.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                )
                .OrderByDescending(x => x.CreateDate).ToList();

        private string searchString;

        protected override async Task OnInitializedAsync()
        {
            Results = await ResultsRepository.GetResultsAsync();
        }

        public void GoToResult(MResult result)
        {
            NavigationManager.NavigateTo($"/results/{result.Id}");
        }
    }
}
