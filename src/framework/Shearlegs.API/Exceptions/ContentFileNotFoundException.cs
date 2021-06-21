using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.API.Exceptions
{
    public class ContentFileNotFoundException : Exception
    {
        public ContentFileNotFoundException(string name) : base($"The content file with the name '{name}' wasn't found")
        {

        }
    }
}
