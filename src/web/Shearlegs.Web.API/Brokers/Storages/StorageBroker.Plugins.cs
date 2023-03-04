using Dapper;
using Microsoft.AspNetCore.Connections.Features;
using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.Plugins.Params;
using Shearlegs.Web.API.Models.Plugins.Results;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        //public async ValueTask<IEnumerable<Plugin>> SelectAllPluginsAsync()
        //{
        //    GetPluginsParams @params = new();

        //    return await GetPluginsAsync(@params);
        //}

        //public async ValueTask<Plugin> SelectPluginByIdAsync(int pluginId)
        //{
        //    GetPluginsParams @params = new()
        //    {
        //        PluginId = pluginId
        //    };

        //    return await GetPluginAsync(@params);
        //}

        //public async ValueTask<Plugin> SelectPluginByPackageIdAsync(string packageId)
        //{
        //    GetPluginsParams @params = new()
        //    {
        //        PackageId = packageId
        //    };

        //    return await GetPluginAsync(@params);
        //}

        public async ValueTask<AddPluginResult> AddPluginAsync(AddPluginParams @params)
        {
            const string sql = "dbo.AddPlugin";

            DynamicParameters dp = StoredProcedureParameters(@params);

            dp.Add("@PluginId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            AddPluginResult result = new();
            result.StoredProcedureResult = await ExecuteStoredProcedureAsync(sql, dp);
            result.PluginId = dp.Get<int?>("@PluginId");

            return result;
        }

        public async ValueTask<StoredProcedureResult> UpdatePluginAsync(UpdatePluginParams @params)
        {
            const string sql = "dbo.UpdatePlugin";

            return await ExecuteStoredProcedureAsync(sql, @params);
        }

        public async ValueTask<Plugin> GetPluginAsync(GetPluginsParams @params)
        {
            const string sql = "dbo.GetPlugins";
            
            Plugin plugin = null;

            await connection.QueryAsync<Plugin, UserInfo, UserInfo, Plugin>(sql, (p, uu, cu) => 
            {
                plugin = p;
                plugin.UpdateUser = uu;
                plugin.CreateUser = cu;

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return plugin;
        }

        public async ValueTask<IEnumerable<Plugin>> GetPluginsAsync(GetPluginsParams @params)
        {
            const string sql = "dbo.GetPlugins";

            return await connection.QueryAsync<Plugin, UserInfo, UserInfo, Plugin>(sql, (p, uu, cu) => 
            {
                p.UpdateUser = uu;
                p.CreateUser = cu;

                return p;
            }, @params, commandType: CommandType.StoredProcedure);
        }
    }
}
