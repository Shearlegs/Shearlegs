using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Exceptions
{
    public class ParameterTypeNotSupportedException : Exception
    {
        public ParameterTypeNotSupportedException(Type type) : base($"Type {type.Name} is not supported as a parameter!")
        {

        }
    }
}
