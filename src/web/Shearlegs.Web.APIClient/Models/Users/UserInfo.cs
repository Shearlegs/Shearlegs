﻿using Shearlegs.Web.Shared.Enums;

namespace Shearlegs.Web.APIClient.Models.Users
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}
