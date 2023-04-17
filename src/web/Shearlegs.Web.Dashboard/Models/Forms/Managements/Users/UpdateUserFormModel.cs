using Shearlegs.Web.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shearlegs.Web.Dashboard.Models.Forms.Managements.Users
{
    public class UpdateUserFormModel
    {
        public UserRole Role { get; set; }
        public string Password { get; set; }
    }
}
