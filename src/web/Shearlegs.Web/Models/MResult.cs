using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Models
{
    public class MResult
    {
        public int Id { get; set; }
        public int VersionId { get; set; }
        public int UserId { get; set; }
        public string ParametersJson { get; set; }
        public string ResultJson { get; set; }
        public string ResultType { get; set; }
        public DateTime CreateDate { get; set; }

        public MVersion Version { get; set; }
        public MUser User { get; set; }
    }
}
