using Newtonsoft.Json;
using Shearlegs.API.Exceptions;
using Shearlegs.API.Plugins.Info;
using Shearlegs.API.Plugins.Parameters;
using Shearlegs.Web.Extensions;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Shearlegs.Web.Models
{
    public class MVersionParameter
    {
        public int VersionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }
        public bool IsArray { get; set; }
        public bool IsRequired { get; set; }
        public bool IsSecret { get; set; }

        public static MVersionParameter FromParameterInfo(IPluginParameterInfo info)
        {
            MVersionParameter parameter = new()
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

            parameter.Value = info.Value?.ToString() ?? null;

            return parameter;
        }
    }
}
