using System;

namespace Shearlegs.Web.API.Models.ShearlegsFrameworks
{
    public class ShearlegsPluginParameterInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public bool IsSecret { get; set; }
        public bool IsArray { get; set; }
    }
}
