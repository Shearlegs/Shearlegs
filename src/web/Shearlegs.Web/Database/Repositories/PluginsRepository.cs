using Dapper;
using Shearlegs.Web.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            return plugin;
        }

        public async Task<IEnumerable<MPlugin>> GetPluginsAsync()
        {
            const string sql = "SELECT p.*, cu.Id, cu.Name, uu.Id, uu.Name " +
                "FROM dbo.Plugins p " +
                "LEFT JOIN dbo.Users cu ON cu.Id = p.CreateUserId " +
                "LEFT JOIN dbo.Users uu ON uu.Id = p.UpdateUserId";

            return await connection.QueryAsync<MPlugin, MUser, MUser, MPlugin>(sql, (p, cu, uu) =>
            {
                p.CreateUser = cu;
                p.UpdateUser = uu;
                return p;
            });
        }
    }
}
