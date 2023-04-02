using Shearlegs.Web.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shearlegs.Web.Dashboard.Models.Forms
{
    public class UpdateUserFormModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public string Password { get; set; }
    }
}
