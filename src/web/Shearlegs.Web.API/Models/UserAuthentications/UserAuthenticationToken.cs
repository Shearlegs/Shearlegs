﻿namespace Shearlegs.Web.API.Models.UserAuthentications
{
    public class UserAuthenticationToken
    {
        public AuthenticatedUser AuthenticatedUser { get; set; }
        public string Token { get; set; }
    }
}