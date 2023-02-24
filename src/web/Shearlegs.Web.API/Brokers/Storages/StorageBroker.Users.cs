using Dapper;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Users.Params;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<IEnumerable<User>> SelectAllUsersAsync()
        {
            const string sql = "SELECT * FROM dbo.Users";

            return await connection.QueryAsync<User>(sql);
        }

        public async ValueTask<User> SelectUserByIdAsync(int userId)
        {
            const string sql = "SELECT * FROM dbo.Users WHERE Id = @userId;";

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { userId });
        }

        public async ValueTask<User> SelectUserByNameAsync(string username)
        {
            const string sql = "SELECT * FROM dbo.Users WHERE Name = @username;";

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { username });
        }

        public async ValueTask<StoredProcedureResult<User>> AddUserAsync(AddUserParams @params)
        {
            const string sql = "dbo.AddUser";

            return await QuerySingleStoredProcedureAsync<User>(sql, @params);
        }
    }
}
