using Shearlegs.API.AssemblyLoading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Core.AssemblyLoading
{
    public class AssemblyContext : IDisposable, IAssemblyContext
    {
        public static AssemblyContext Create() => new AssemblyContext();

        private readonly HostAssemblyLoadContext context;
        private AssemblyContext()
        {
            context = new HostAssemblyLoadContext();
        }

        public void Unload() => context.Unload();
        public Assembly LoadAssembly(Stream assembly) => context.LoadFromStream(assembly);

        public void Dispose() => Unload();
    }
}
