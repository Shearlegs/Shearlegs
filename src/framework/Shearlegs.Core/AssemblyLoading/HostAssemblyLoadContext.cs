using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.AssemblyLoading
{
    // Got it from:
    // https://github.com/dotnet/samples/blob/main/core/tutorials/Unloading/Host/Program.cs    
    public class HostAssemblyLoadContext : AssemblyLoadContext
    {
        public HostAssemblyLoadContext() : base(isCollectible: true)
        {
        }

        protected override Assembly Load(AssemblyName assemblyName) => null;
    }
}
