using NuGet.Packaging.Core;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.NuGet
{
    public class PackageConfiguration
    {
        public string Package { get; set; }
        public VersionRange VersionRange { get; set; }
        public bool PreRelease { get; set; }

        public static PackageConfiguration FromPackageDependency(PackageDependency dependency)
        {
            return new PackageConfiguration()
            {
                Package = dependency.Id,
                VersionRange = dependency.VersionRange,
                PreRelease = false
            };
        }
    }
}
