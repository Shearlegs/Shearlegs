using NuGet.Packaging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.NuGet
{
    public class NuGetAssembly : Assembly
    {
        public PackageIdentity Identity { get; set; }
    }
}
