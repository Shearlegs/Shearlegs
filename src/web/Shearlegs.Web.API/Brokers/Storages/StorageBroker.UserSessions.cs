﻿using Dapper;
using Shearlegs.Web.API.Models.PluginSecrets.Params;
using Shearlegs.Web.API.Models.PluginSecrets;
using Shearlegs.Web.API.Models.UserSessions;
using Shearlegs.Web.API.Models.UserSessions.Params;
using Shearlegs.Web.API.Models.UserSessions.Results;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Shearlegs.Web.API.Utilities.StoredProcedures;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<CreateUserSessionResult> CreateUserSessionAsync(CreateUserSessionParams @params)
        {
            const string sql = "dbo.CreateUserSession";

            CreateUserSessionResult result = new();

            DynamicParameters param = StoredProcedureParameters(@params);
            param.Add("@SessionId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            result.StoredProcedureResult = await ExecuteStoredProcedureAsync(sql, param);

            result.SessionId = param.Get<Guid?>("@SessionId");

            return result;
        }

        public async ValueTask<StoredProcedureResult> RevokeUserSessionAsync(RevokeUserSessionParams @params)
        {
            const string sql = "dbo.RevokeUserSession";

            return await ExecuteStoredProcedureAsync(sql, @params);
        }

        public async ValueTask<UserSession> GetUserSessionAsync(GetUserSessionsParams @params)
        {
            const string sql = "dbo.GetUserSessions";

            return await connection.QuerySingleOrDefaultAsync<UserSession>(sql, @params, commandType: CommandType.StoredProcedure);
        }

        public async ValueTask<IEnumerable<UserSession>> GetUserSessionsAsync(GetUserSessionsParams @params)
        {
            const string sql = "dbo.GetUserSessions";

            return await connection.QueryAsync<UserSession>(sql, @params, commandType: CommandType.StoredProcedure);
        }
    }
}