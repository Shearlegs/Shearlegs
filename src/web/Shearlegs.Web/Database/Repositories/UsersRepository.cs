using Dapper;
using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Database.Repositories
{
    public class UsersRepository
    {
        private readonly SqlConnection connection;

        public UsersRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task<MUser> GetUserAsync(string name, string password)
        {
            const string sql = "dbo.GetUser";
            return await connection.QuerySingleOrDefaultAsync<MUser>(sql, new { name, password }, commandType: CommandType.StoredProcedure);
        }

        public async Task<MUser> AddUserAsync(MUser user)
        {
            const string sql = "dbo.CreateUser";
            return await connection.QuerySingleOrDefaultAsync<MUser>(sql, new { user.Name, user.Password, user.Role }, commandType: CommandType.StoredProcedure);
        }

        public async Task<MUser> UpdateUserAsync(MUser user)
        {
            const string sql = "dbo.UpdateUser";
            return await connection.QuerySingleOrDefaultAsync<MUser>(sql, new { user.Id, user.Name, user.Password, user.Role }, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<MUser>> GetUsersAsync()
        {
            const string sql = "SELECT Id, Name, Role, LastLoginDate, UpdateDate, CreateDate FROM dbo.Users;";
            return await connection.QueryAsync<MUser>(sql);
        }

        public async Task<MUser> GetUserAsync(int userId)
        {
            const string sql = "SELECT Id, Name, Role, LastLoginDate, UpdateDate, CreateDate FROM dbo.Users WHERE Id = @userId;";
            return await connection.QuerySingleOrDefaultAsync<MUser>(sql, new { userId });
        }

        public async Task UpdateLastLoginDateAsync(int userId)
        {
            const string sql = "UPDATE dbo.Users SET LastLoginDate = SYSDATETIME() WHERE Id = @userId;";
            await connection.ExecuteAsync(sql, new { userId });
        }
    }
}
