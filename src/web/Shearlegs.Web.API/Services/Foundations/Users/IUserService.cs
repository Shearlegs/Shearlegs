﻿using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Users.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> CreateUserAsync(CreateUserParams @params);
        ValueTask<User> ModifyUserIdentityAsync(ModifyUserIdentityParams @params);
        ValueTask<IEnumerable<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask<User> RetrieveUserByNameAsync(string username);
    }
}