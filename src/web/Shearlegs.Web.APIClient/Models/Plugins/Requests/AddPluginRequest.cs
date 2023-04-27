﻿namespace Shearlegs.Web.APIClient.Models.Plugins.Requests
{
    public class AddPluginRequest
    {
        public string PackageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int? CreateUserId { get; set; }
    }
}