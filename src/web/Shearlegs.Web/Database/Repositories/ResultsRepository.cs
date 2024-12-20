﻿using Dapper;
using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Database.Repositories
{
    public class ResultsRepository
    {
        private readonly SqlConnection connection;

        public ResultsRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task<int> AddResultAsync(MResult result)
        {
            const string sql = "INSERT INTO dbo.Results (VersionId, UserId, ParametersJson, ResultJson, ResultType) " +
                "OUTPUT INSERTED.Id " + 
                "VALUES (@VersionId, @UserId, @ParametersJson, @ResultJson, @ResultType)";

            return await connection.ExecuteScalarAsync<int>(sql, result);
        }

        public async Task<bool> IsResultUserAsync(int resultId, int userId)
        {
            const string sql = "SELECT COUNT(*) FROM dbo.Results WHERE Id = @resultId AND UserId = @userId;";
            return await connection.ExecuteScalarAsync<bool>(sql, new { resultId, userId });
        }

        public async Task<MResult> GetResultAsync(int resultId)
        {
            const string sql = "SELECT r.*, v.*, p.*, u.* " +
                "FROM dbo.Results r " +
                "JOIN dbo.Versions v ON v.Id = r.VersionId " +
                "JOIN dbo.Plugins p ON p.Id = v.PluginId " +
                "JOIN dbo.Users u ON u.Id = r.UserId " +
                "WHERE r.Id = @resultId;";

            return (await connection.QueryAsync<MResult, MVersion, MPlugin, MUser, MResult>(sql, (r, v, p, u) => 
            {
                r.Version = v;
                r.Version.Plugin = p;
                r.User = u;
                return r;
            }, new { resultId })).FirstOrDefault();
        }

        public async Task<IEnumerable<MResult>> GetUserResultsAsync(int userId)
        {
            const string sql = "dbo.GetUserResults";

            return await connection.QueryAsync<MResult, MVersion, MPlugin, MUser, MResult>(sql, (r, v, p, u) =>
            {
                r.Version = v;
                r.Version.Plugin = p;
                r.User = u;
                return r;
            }, new { userId }, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<MResult>> GetResultsAsync()
        {
            const string sql = "dbo.GetResults";

            return await connection.QueryAsync<MResult, MVersion, MPlugin, MUser, MResult>(sql, (r, v, p, u) =>
            {
                r.Version = v;
                r.Version.Plugin = p;
                r.User = u;
                return r;
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
