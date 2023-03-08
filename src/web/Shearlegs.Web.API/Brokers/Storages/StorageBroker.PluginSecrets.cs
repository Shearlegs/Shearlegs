using Dapper;
using Shearlegs.Web.API.Models.Plugins.Params;
using Shearlegs.Web.API.Models.Plugins.Results;
using Shearlegs.Web.API.Models.PluginSecrets;
using Shearlegs.Web.API.Models.PluginSecrets.Params;
using Shearlegs.Web.API.Models.PluginSecrets.Results;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<AddPluginSecretResult> AddPluginSecretAsync(AddPluginSecretParams @params)
        {
            const string sql = "dbo.AddPluginSecret";

            DynamicParameters dp = StoredProcedureParameters(@params);

            dp.Add("@PluginSecretId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            AddPluginSecretResult result = new();
            result.StoredProcedureResult = await ExecuteStoredProcedureAsync(sql, dp);
            result.PluginSecretId = dp.Get<int?>("@PluginSecretId");

            return result;
        }

        public async ValueTask<StoredProcedureResult> UpdatePluginSecretAsync(UpdatePluginSecretParams @params)
        {
            const string sql = "dbo.UpdatePluginSecret";

            return await ExecuteStoredProcedureAsync(sql, @params);
        }

        public async ValueTask<PluginSecret> GetPluginSecretAsync(GetPluginSecretsParams @params)
        {
            const string sql = "dbo.GetPluginSecrets";

            PluginSecret pluginSecret = null;

            await connection.QueryAsync<PluginSecret, UserInfo, PluginSecret>(sql, (ps, cu) =>
            {
                pluginSecret = ps;
                pluginSecret.CreateUser = cu;

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return pluginSecret;
        }

        public async ValueTask<IEnumerable<PluginSecret>> GetPluginSecretsAsync(GetPluginSecretsParams @params)
        {
            const string sql = "dbo.GetPluginSecrets";

            return await connection.QueryAsync<PluginSecret, UserInfo, PluginSecret>(sql, (p, cu) =>
            {
                p.CreateUser = cu;

                return p;
            }, @params, commandType: CommandType.StoredProcedure);
        }
    }
}
