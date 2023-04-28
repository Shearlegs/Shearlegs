using Dapper;
using Shearlegs.Web.API.Models.Nodes;
using Shearlegs.Web.API.Models.Nodes.Params;
using Shearlegs.Web.API.Models.Nodes.Results;
using Shearlegs.Web.API.Models.Plugins.Params;
using Shearlegs.Web.API.Models.Plugins.Results;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<AddNodeResult> AddNodeAsync(AddNodeParams @params)
        {
            const string sql = "dbo.AddNode";

            DynamicParameters dp = StoredProcedureParameters(@params);

            dp.Add("@NodeId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            AddNodeResult result = new();
            result.StoredProcedureResult = await ExecuteStoredProcedureAsync(sql, dp);
            result.NodeId = dp.Get<int?>("@NodeId");

            return result;
        }

        public async ValueTask<StoredProcedureResult> UpdateNodeAsync(UpdateNodeParams @params)
        {
            const string sql = "dbo.UpdateNode";

            return await ExecuteStoredProcedureAsync(sql, @params);
        }

        public async ValueTask<Node> GetNodeAsync(GetNodesParams @params)
        {
            const string sql = "dbo.GetNodes";

            Node node = null;

            await connection.QueryAsync<Node, UserInfo, UserInfo, Node>(sql, (p, uu, cu) =>
            {
                node = p;
                node.UpdateUser = uu;
                node.CreateUser = cu;

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return node;
        }

        public async ValueTask<IEnumerable<Node>> GetNodesAsync(GetNodesParams @params)
        {
            const string sql = "dbo.GetNodes";

            return await connection.QueryAsync<Node, UserInfo, UserInfo, Node>(sql, (n, uu, cu) =>
            {
                n.UpdateUser = uu;
                n.CreateUser = cu;

                return n;
            }, @params, commandType: CommandType.StoredProcedure);
        }
    }
}
