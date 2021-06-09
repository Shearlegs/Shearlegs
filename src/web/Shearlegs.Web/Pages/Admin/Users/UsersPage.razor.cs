using Microsoft.AspNetCore.Components;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Pages.Admin.Users
{
    public partial class UsersPage
    {
        [Inject]
        public UsersRepository UsersRepository { get; set; }

        public IEnumerable<User> Users { get; set; }

        private string searchString;

        public List<User> SearchedUsers 
            => Users.Where(x => string.IsNullOrEmpty(searchString) || x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

        protected override async Task OnInitializedAsync()
        {
            Users = await UsersRepository.GetUsersAsync();
        }
    }
}
