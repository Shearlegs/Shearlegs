using Shearlegs.Web.APIClient.Models.Users;
using Shearlegs.Web.APIClient.Models.VersionParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Models.Versions
{
    public class Version
    {
        public int Id { get; set; }
        public int PluginId { get; set; }
        public string Name { get; set; }
        public int ContentLength { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public UserInfo CreateUser { get; set; }
        public List<VersionParameter> Parameters { get; set; }
    }
}
