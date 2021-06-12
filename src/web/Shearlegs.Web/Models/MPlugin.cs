using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Models
{
    public class MPlugin
    {
        public int Id { get; set; }
        public string PackageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }


        public MUser CreateUser { get; set; }
        public MUser UpdateUser { get; set; }

        public List<MVersion> Versions { get; set; }
    }
}
