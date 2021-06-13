using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Models
{
    public class MPlugin
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string PackageId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(2000)]
        public string Description { get; set; }
        public string Author { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }


        public MUser CreateUser { get; set; }
        public MUser UpdateUser { get; set; }

        public List<MVersion> Versions { get; set; }

        public MPlugin MakeCopy()
        {
            return new MPlugin()
            {
                Id = Id,
                PackageId = PackageId,
                Name = Name,
                Description = Description,
                Author = Author,                
                CreateUserId = CreateUserId,
                CreateDate = CreateDate,
                UpdateUserId = UpdateUserId,
                UpdateDate = UpdateDate
            };
        }
    }
}
