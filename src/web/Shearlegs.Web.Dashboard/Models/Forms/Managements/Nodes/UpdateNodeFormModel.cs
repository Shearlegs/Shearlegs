using System.ComponentModel.DataAnnotations;

namespace Shearlegs.Web.Dashboard.Models.Forms.Managements.Nodes
{
    public class UpdateNodeFormModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public string FQDN { get; set; }
        [Required]
        public string Scheme { get; set; }
        [Required]
        public int HttpPort { get; set; }
        [Required]
        public int HttpsPort { get; set; }
        [Required]
        public int CacheSizeLimit { get; set; }
        public bool IsBehindProxy { get; set; }
        public bool IsEnabled { get; set; }
    }
}
