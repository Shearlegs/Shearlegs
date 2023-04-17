using Shearlegs.Web.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shearlegs.Web.Dashboard.Models.Forms.Managements.Users
{
    public class CreateUserFormModel
    {
        [Required]
        [MaxLength(32, ErrorMessage = "Name must not exceed 32 characters")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
