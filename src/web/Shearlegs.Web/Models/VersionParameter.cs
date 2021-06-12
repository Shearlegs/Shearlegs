using Shearlegs.API.Exceptions;
using Shearlegs.API.Plugins.Parameters;
using Shearlegs.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Web.Models
{
    public class VersionParameter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }
        public byte[] Value { get; set; }
        public bool IsArray { get; set; }
        public bool IsRequired { get; set; }
        public bool IsSecret { get; set; }

        public static VersionParameter FromParameterInfo(PluginParameterInfo info)
        {
            VersionParameter parameter = new VersionParameter()
            {
                Name = info.Name,
                Description = info.Description,
                DataType = info.Type.Name,
                IsRequired = info.IsRequired,
                IsSecret = info.IsSecret
            };

            if (info.Type.IsNumericType())
            {
                parameter.InputType = "number";
            } else if (info.Type.IsTextType())
            {
                parameter.InputType = "text";
            } else if (info.Type.IsCheckType())
            {
                parameter.InputType = "checkbox";
            } else if (info.Type == typeof(FileParameter))
            {
                parameter.InputType = "file";
            } else
            {
                throw new ParameterTypeNotSupportedException(info.Type);
            }

            return parameter;
        }
    }
}
