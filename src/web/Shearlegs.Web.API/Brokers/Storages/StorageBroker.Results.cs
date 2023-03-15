using Dapper;
using Shearlegs.Web.API.Models.Plugins;
using Shearlegs.Web.API.Models.Results;
using Shearlegs.Web.API.Models.Results.Params;
using Shearlegs.Web.API.Models.Results.Results;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<ResultParametersData> SelectResultParametersDataByIdAsync(int resultId)
        {
            const string sql = "SELECT ParametersData FROM dbo.Results WHERE Id = @resultId;";

            return await connection.QuerySingleOrDefaultAsync<ResultParametersData>(sql, new { resultId });
        }

        public async ValueTask<ResultContentData> SelectResultContentDataByIdAsync(int resultId)
        {
            const string sql = "SELECT ResultData, ResultType FROM dbo.Results WHERE Id = @resultId;";

            return await connection.QuerySingleOrDefaultAsync<ResultContentData>(sql, new { resultId });
        }

        public async ValueTask<AddResultResult> AddResultAsync(AddResultParams @params)
        {
            const string sql = "dbo.AddResult";

            DynamicParameters dp = StoredProcedureParameters(@params);

            dp.Add("@ResultId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            AddResultResult result = new();
            result.StoredProcedureResult = await ExecuteStoredProcedureAsync(sql, dp);
            result.ResultId = dp.Get<int?>("@ResultId");

            return result;
        }

        public async ValueTask<StoredProcedureResult> UpdateResultAsync(UpdateResultParams @params)
        {
            const string sql = "dbo.UpdateResult";

            StoredProcedureResult result = await ExecuteStoredProcedureAsync(sql, @params);

            return result;
        }

        public async ValueTask<StoredProcedureResult> UpdateResultStatusAsync(UpdateResultStatusParams @params)
        {
            const string sql = "dbo.UpdateResultStatus";

            StoredProcedureResult result = await ExecuteStoredProcedureAsync(sql, @params);

            return result;
        }

        public async ValueTask<Result> GetResultAsync(GetResultsParams @params)
        {
            const string sql = "dbo.GetResults";

            Result result = null;

            await connection.QueryAsync<Result, UserInfo, VersionInfo, PluginInfo, Result>(sql, (r, u, v, p) =>
            {
                result = r;
                result.User = u;
                result.Version = v;
                result.Plugin = p;

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async ValueTask<IEnumerable<Result>> GetResultsAsync(GetResultsParams @params)
        {
            const string sql = "dbo.GetResults";

            List<Result> results = new();

            await connection.QueryAsync<Result, UserInfo, VersionInfo, PluginInfo, Result>(sql, (r, u, v, p) =>
            {
                Result result = results.FirstOrDefault(x => x.Id == r.Id);
                
                if (result == null)
                {
                    result = r;
                    results.Add(result);
                }

                result.User = u;
                result.Version = v;
                result.Plugin = p;

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return results;
        }
    }
}
