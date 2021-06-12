using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Exceptions
{
    public class MultipleParametersTypeException : Exception
    {
        public MultipleParametersTypeException() : base("The plugin assembly can have only one class with Parameters attribute")
        {

        }
    }
}
