using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Users.Params;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<IEnumerable<User>> SelectAllUsersAsync();
        ValueTask<User> SelectUserByIdAsync(int userId);
        ValueTask<User> SelectUserByNameAsync(string username);
        ValueTask<StoredProcedureResult<User>> AddUserAsync(AddUserParams @params);
    }
}
