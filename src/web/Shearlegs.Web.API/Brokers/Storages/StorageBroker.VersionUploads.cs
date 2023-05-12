﻿using Dapper;
using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Models.Versions.Params;
using Shearlegs.Web.API.Models.Versions.Results;
using Shearlegs.Web.API.Models.VersionUploads;
using Shearlegs.Web.API.Models.VersionUploads.Params;
using Shearlegs.Web.API.Models.VersionUploads.Results;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<IEnumerable<VersionUpload>> GetVersionUploadsAsync(GetVersionUploadsParams @params)
        {
            const string sql = "dbo.GetVersionUploads";

            List<VersionUpload> versionUploads = new();

            await connection.QueryAsync<VersionUpload, UserInfo, NodeInfo, VersionUpload>(sql, (vu, u, n) =>
            {
                VersionUpload versionUpload = versionUploads.FirstOrDefault(x => x.Id == vu.Id);

                if (versionUpload == null)
                {
                    versionUpload = vu;
                    versionUpload.User = u;
                    versionUpload.Node = n;

                    versionUploads.Add(versionUpload);
                }

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return versionUploads;
        }

        public async ValueTask<VersionUpload> GetVersionUploadAsync(GetVersionUploadsParams @params)
        {
            const string sql = "dbo.GetVersionUploads";

            VersionUpload versionUpload = null;

            await connection.QueryAsync<VersionUpload, UserInfo, NodeInfo, VersionUpload>(sql, (vu, u, n) =>
            {
                versionUpload = vu;
                versionUpload.User = u;
                versionUpload.Node = n;

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return versionUpload;
        }

        public async ValueTask<AddVersionUploadResult> AddVersionUploadAsync(AddVersionUploadParams @params)
        {
            const string sql = "dbo.AddVersionUpload";

            DynamicParameters dp = StoredProcedureParameters(@params);
            dp.Add("@VersionUploadId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            AddVersionUploadResult result = new();
            result.StoredProcedureResult = await ExecuteStoredProcedureAsync(sql, dp);
            result.VersionUploadId = dp.Get<int?>("@VersionUploadId");

            return result;
        }
    }
}