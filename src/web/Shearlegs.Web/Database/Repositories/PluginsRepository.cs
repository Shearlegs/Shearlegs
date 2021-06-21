using Dapper;
using Shearlegs.Web.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Database.Repositories
{
    public class PluginsRepository
    {
        private readonly SqlConnection connection;

        public PluginsRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task<MPlugin> AddPluginAsync(MPlugin plugin)
        {
            const string sql = "INSERT INTO dbo.Plugins (PackageId, Name, Description, Author, CreateUserId, UpdateUserId) " +
                "OUTPUT INSERTED.Id, INSERTED.PackageId, INSERTED.Name, INSERTED.Description, INSERTED.Author, INSERTED.CreateUserId, " +
                "INSERTED.CreateDate, INSERTED.UpdateUserId, INSERTED.UpdateDate " + 
                "VALUES (@PackageId, @Name, @Description, @Author, @CreateUserId, @UpdateUserId);";
            return await connection.QuerySingleOrDefaultAsync<MPlugin>(sql, plugin);
        }

        public async Task<MPlugin> UpdatePluginAsync(MPlugin plugin)
        {
            const string sql = "UPDATE dbo.Plugins SET Name = @Name, Description = @Description, " +
                "Author = @Author, UpdateUserId = @UpdateUserId, UpdateDate = SYSDATETIME() WHERE Id = @Id;";
            await connection.ExecuteAsync(sql, plugin);
            return await GetPluginAsync(plugin.Id);
        }

        public async Task<MPlugin> GetPluginAsync(int pluginId)
        {
            const string sql = "SELECT p.*, cu.Id, cu.Name, uu.Id, uu.Name, v.Id, v.PluginId, v.Name, v.CreateUserId, v.CreateDate " +
                "FROM dbo.Plugins p " +
                "LEFT JOIN dbo.Users cu ON cu.Id = p.CreateUserId " +
                "LEFT JOIN dbo.Users uu ON uu.Id = p.UpdateUserId " + 
                "LEFT JOIN dbo.Versions v ON v.PluginId = p.Id " +
                "WHERE p.Id = @pluginId";

            const string sql1 = "SELECT * FROM dbo.PluginSecrets WHERE PluginId = @pluginId;";

            MPlugin plugin = null;
            await connection.QueryAsync<MPlugin, MUser, MUser, MVersion, MPlugin>(sql, (p, cu, uu, v) => 
            {
                if (plugin == null)
                {
                    plugin = p;
                    plugin.CreateUser = cu;
                    plugin.UpdateUser = uu;                    
                    plugin.Versions = new List<MVersion>();
                }

                if (v != null)
                    plugin.Versions.Add(v);

                return null;
            }, new { pluginId });

            plugin.Secrets = (await connection.QueryAsync<MPluginSecret>(sql1, new { pluginId })).ToList();

            return plugin;
        }

        public async Task<MPlugin> GetPluginAsync(string packageId)
        {
            const string sql = "SELECT p.*, v.* FROM dbo.Plugins p " +
                "LEFT JOIN dbo.Versions v ON v.PluginId = p.Id " +
                "WHERE p.PackageId = @packageId;";
            MPlugin plugin = null;
            await connection.QueryAsync<MPlugin, MVersion, MPlugin>(sql, (p, v) => 
            { 
                if (plugin == null)
                {
                    plugin = p;
                    plugin.Versions = new List<MVersion>();
                }

                if (v != null)
                    plugin.Versions.Add(v);

                return null;
            }, new { packageId });

            return plugin;
        }

        public async Task<IEnumerable<MPlugin>> GetPluginsAsync()
        {
            const string sql = "SELECT p.*, cu.Id, cu.Name, uu.Id, uu.Name " +
                "FROM dbo.Plugins p " +
                "LEFT JOIN dbo.Users cu ON cu.Id = p.CreateUserId " +
                "LEFT JOIN dbo.Users uu ON uu.Id = p.UpdateUserId;";

            return await connection.QueryAsync<MPlugin, MUser, MUser, MPlugin>(sql, (p, cu, uu) =>
            {
                p.CreateUser = cu;
                p.UpdateUser = uu;
                return p;
            });
        }

        public async Task<IEnumerable<MPlugin>> GetUserPluginsAsync(int userId)
        {
            string sql = "SELECT p.*, cu.Id, cu.Name, uu.Id, uu.Name " +
                "FROM dbo.Plugins p " +
                "LEFT JOIN dbo.Users cu ON cu.Id = p.CreateUserId " +
                "LEFT JOIN dbo.Users uu ON uu.Id = p.UpdateUserId " +
                "JOIN dbo.UserPlugins up ON up.PluginId = p.Id AND up.UserId = @userId;";                

            return await connection.QueryAsync<MPlugin, MUser, MUser, MPlugin>(sql, (p, cu, uu) =>
            {
                p.CreateUser = cu;
                p.UpdateUser = uu;
                return p;
            }, new { userId });
        }

        public async Task<bool> IsUserPluginAsync(int userId, int pluginId)
        {
            const string sql = "SELECT COUNT(*) FROM dbo.UserPlugins WHERE UserId = @userId AND PluginId = @pluginId;";
            return await connection.ExecuteScalarAsync<bool>(sql, new { userId, pluginId });
        }

        public async Task<MPluginSecret> AddPluginSecretAsync(MPluginSecret secret)
        {
            const string sql = "INSERT INTO dbo.PluginSecrets (PluginId, Name, Value, IsArray, CreateUserId) " +
                "OUTPUT INSERTED.Id, INSERTED.PluginId, INSERTED.Name, INSERTED.Value, INSERTED.IsArray, " +
                "INSERTED.CreateUserId, INSERTED.CreateDate " +
                "VALUES (@PluginId, @Name, @Value, @IsArray, @CreateUserId);";
            return await connection.QuerySingleOrDefaultAsync<MPluginSecret>(sql, secret);
        }

        public async Task DeletePluginSecretAsync(int secretId)
        {
            const string sql = "DELETE FROM dbo.PluginSecrets WHERE Id = @secretId;";
            await connection.ExecuteAsync(sql, new { secretId });
        }
    }
}
