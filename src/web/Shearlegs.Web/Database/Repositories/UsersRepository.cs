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
        protected readonly SqlConnection connection;

        public UsersRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public virtual async Task<MUser> GetUserAsync(string name, string password)
        {
            const string sql = "dbo.GetUser";
            return await connection.QuerySingleOrDefaultAsync<MUser>(sql, new { name, password }, commandType: CommandType.StoredProcedure);
        }

        public async Task<MUser> AddUserAsync(MUser user)
        {
            const string sql = "dbo.CreateUser";
            return await connection.QuerySingleOrDefaultAsync<MUser>(sql, new { user.Name, user.Password, user.Role, user.AuthenticationType }, 
                commandType: CommandType.StoredProcedure);
        }

        public async Task<MUser> UpdateUserAsync(MUser user)
        {
            const string sql = "dbo.UpdateUser";
            return await connection.QuerySingleOrDefaultAsync<MUser>(sql, new { user.Id, user.Name, user.Password, user.Role }, 
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<MUser>> GetUsersAsync()
        {
            const string sql = "SELECT Id, Name, Role, AuthenticationType, LastLoginDate, UpdateDate, CreateDate FROM dbo.Users;";
            return await connection.QueryAsync<MUser>(sql);
        }

        public virtual async Task<MUser> GetUserAsync(string username)
        {
            const string sql = "SELECT Id, Name, Role, AuthenticationType, LastLoginDate, UpdateDate, CreateDate FROM dbo.Users WHERE Name = @username";
            return await connection.QuerySingleOrDefaultAsync<MUser>(sql, new { username });
        }

        public async Task<MUser> GetUserAsync(int userId)
        {
            const string sql = "SELECT u.*, up.*, p.* FROM dbo.Users u " +
                "LEFT JOIN dbo.UserPlugins up ON u.Id = up.UserId " +
                "LEFT JOIN dbo.Plugins p ON p.Id = up.PluginId " +
                "WHERE u.Id = @userId;";
            MUser user = null;
            await connection.QueryAsync<MUser, MUserPlugin, MPlugin, MUser>(sql, (u, up, p) => 
            { 
                if (user == null)
                {
                    user = u;
                    user.Plugins = new List<MUserPlugin>();
                }

                if (up != null)
                {
                    up.Plugin = p;
                    user.Plugins.Add(up);
                }

                return null;
            }, new { userId });
            return user;
        }

        public async Task UpdateLastLoginDateAsync(int userId)
        {
            const string sql = "UPDATE dbo.Users SET LastLoginDate = SYSDATETIME() WHERE Id = @userId;";
            await connection.ExecuteAsync(sql, new { userId });
        }

        public async Task<MUserPlugin> AddUserPluginAsync(MUserPlugin userPlugin)
        {
            const string sql = "INSERT INTO dbo.UserPlugins (UserId, PluginId, CreateUserId) " +
                "OUTPUT INSERTED.Id " + 
                "VALUES (@UserId, @PluginId, @CreateUserId);";
            return await GetUserPluginAsync(await connection.ExecuteScalarAsync<int>(sql, userPlugin));
        }

        public async Task<MUserPlugin> GetUserPluginAsync(int userPluginId)
        {
            const string sql = "SELECT up.*, p.* FROM dbo.UserPlugins up " +
                "JOIN dbo.Plugins p ON p.Id = up.PluginId " +
                "WHERE up.Id = @userPluginId";

            return (await connection.QueryAsync<MUserPlugin, MPlugin, MUserPlugin>(sql, (up, p) => 
            {
                up.Plugin = p;
                return up;
            }, new { userPluginId })).FirstOrDefault();
        }

        public async Task DeleteUserPluginAsync(int userPluginId)
        {
            const string sql = "DELETE FROM dbo.UserPlugins WHERE Id = @userPluginId;";
            await connection.ExecuteAsync(sql, new { userPluginId });
        }
    }
}
