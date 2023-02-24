using Shearlegs.Web.API.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Users
{
    public interface IUserService
    {
        ValueTask<IEnumerable<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByIdAsync(int userId);
        ValueTask<User> RetrieveUserByNameAsync(string username);
    }
}