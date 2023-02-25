using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.API.Models.Users.Params
{
    public class AddUserWithPasswordParams
    {
        public string Name  { get; set; }
        public UserRole Role { get; set; }
        public string PasswordText { get; set; }
    }
}
