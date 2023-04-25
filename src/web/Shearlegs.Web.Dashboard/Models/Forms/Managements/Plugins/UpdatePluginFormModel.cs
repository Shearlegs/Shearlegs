using System.ComponentModel.DataAnnotations;

namespace Shearlegs.Web.Dashboard.Models.Forms.Managements.Plugins
{
    public class UpdatePluginFormModel
    {
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Author { get; set; }
    }
}
