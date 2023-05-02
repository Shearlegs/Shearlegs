using System.ComponentModel.DataAnnotations;

namespace Shearlegs.Web.Dashboard.Models.Forms.Managements.NodeVariables
{
    public class AddNodeVariableFormModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Value { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public bool IsSensitive { get; set; }
    }
}
