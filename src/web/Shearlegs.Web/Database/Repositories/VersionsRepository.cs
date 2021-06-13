using Dapper;
using Shearlegs.Web.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Shearlegs.Web.Database.Repositories
{
    public class VersionsRepository
    {
        private readonly SqlConnection connection;

        public VersionsRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task<MVersion> AddVersionAsync(MVersion version)
        {
            const string sql = "INSERT INTO dbo.Versions (PluginId, Name, PackageContent, CreateUserId) " +
                "OUTPUT INSERTED.Id " +
                "VALUES (@PluginId, @Name, @PackageContent, @CreateUserId)";

            version.Id = await connection.ExecuteScalarAsync<int>(sql, version);

            await AddVersionParametersAsync(version.Parameters, version.Id);

            return await GetVersionAsync(version.Id);
        }

        public async Task AddVersionParametersAsync(IEnumerable<MVersionParameter> parameters, int versionId)
        {
            const string sql = "INSERT INTO dbo.VersionParameters (VersionId, Name, Description, InputType, DataType, Value, IsArray, IsRequired, IsSecret) " +
                "OUTPUT INSERTED.Id, INSERTED.VersionId, INSERTED.Name, INSERTED.Description, INSERTED.InputType, " +
                "INSERTED.DataType, INSERTED.Value, INSERTED.IsArray, INSERTED.IsRequired, INSERTEd.IsSecret " +
                "VALUES (@VersionId, @Name, @Description, @InputType, @DataType, @Value, @IsArray, @IsRequired, @IsSecret);";

            foreach (MVersionParameter parameter in parameters)
            {
                parameter.VersionId = versionId;
                await connection.QuerySingleOrDefaultAsync(sql, parameter);
            }
        }

        public async Task<MVersion> GetVersionAsync(int versionId)
        {
            const string sql = "SELECT v.Id, v.PluginId, v.Name, v.CreateUserId, v.CreateDate, p.* " +
                "FROM dbo.Versions v LEFT JOIN dbo.VersionParameters p ON p.VersionId = v.Id " +
                "WHERE v.Id = @versionId;";

            MVersion version = null;
            await connection.QueryAsync<MVersion, MVersionParameter, MVersion>(sql, (v, p) => 
            {
                if (version == null)
                {
                    version = v;
                    version.Parameters = new List<MVersionParameter>();
                }

                if (p != null)
                {
                    version.Parameters.Add(p);
                }

                return null;
            }, new { versionId });

            return version;
        }

        public async Task<byte[]> GetVersionPackageContent(int versionId)
        {
            const string sql = "SELECT PackageContent FROM dbo.Versions WHERE Id = @versionId;";
            return await connection.ExecuteScalarAsync<byte[]>(sql, new { versionId });
        }
    }
}
