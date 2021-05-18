using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.AssemblyLoading
{
    public interface IAssemblyContext
    {
        Assembly LoadAssembly(Stream assembly);
        void Unload();
    }
}
