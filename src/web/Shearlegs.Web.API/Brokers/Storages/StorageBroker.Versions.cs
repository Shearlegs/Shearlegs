using Dapper;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.VersionParameters;
using Shearlegs.Web.API.Models.Versions;
using Shearlegs.Web.API.Models.Versions.Params;
using Shearlegs.Web.API.Models.Versions.Results;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<VersionContent> SelectVersionContentByIdAsync(int versionId)
        {
            const string sql = "SELECT Content FROM dbo.Versions WHERE Id = @versionId;";

            return await connection.QuerySingleOrDefaultAsync<VersionContent>(sql, new { versionId });
        }

        public async ValueTask<AddVersionResult> AddVersionAsync(AddVersionParams @params)
        {
            const string sql = "dbo.AddVersion";

            DynamicParameters dp = StoredProcedureParameters(@params);
            dp.Add("@VersionId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            AddVersionResult result = new();
            result.StoredProcedureResult = await ExecuteStoredProcedureAsync(sql, dp);
            result.VersionId = dp.Get<int?>("@VersionId");

            return result;
        }

        public async ValueTask<Version> GetVersionAsync(GetVersionsParams @params)
        {
            const string sql = "dbo.GetVersions";

            Version version = null;

            await connection.QueryAsync<Version, UserInfo, VersionParameter, Version>(sql, (v, cu, vp) => 
            { 
                if (version == null)
                {
                    version = v;
                    version.CreateUser = cu;
                    version.Parameters = new();
                }

                if (vp != null)
                {
                    version.Parameters.Add(vp);
                }

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return version;
        }

        public async ValueTask<IEnumerable<Version>> GetVersionsAsync(GetVersionsParams @params)
        {
            const string sql = "dbo.GetVersions";

            List<Version> versions = new();

            await connection.QueryAsync<Version, UserInfo, VersionParameter, Version>(sql, (v, cu, vp) =>
            {
                Version version = versions.FirstOrDefault(x => x.Id == v.Id);

                if (version == null)
                {
                    version = v;
                    version.CreateUser = cu;
                    version.Parameters = new();

                    versions.Add(version);
                }

                if (vp != null)
                {
                    version.Parameters.Add(vp);
                }

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return versions;
        }
    }
}
