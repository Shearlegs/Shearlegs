﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shearlegs.Web.APIClient.Models.VersionParameters
{
    public class VersionParameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
        public bool IsArray { get; set; }
        public bool IsRequired { get; set; }
        public bool IsSecret { get; set; }
    }
}
