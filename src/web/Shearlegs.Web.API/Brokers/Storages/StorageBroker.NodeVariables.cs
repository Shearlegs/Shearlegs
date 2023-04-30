using Dapper;
using Shearlegs.Web.API.Models.Nodes.Params;
using Shearlegs.Web.API.Models.Nodes.Results;
using Shearlegs.Web.API.Models.NodeVariables;
using Shearlegs.Web.API.Models.NodeVariables.Params;
using Shearlegs.Web.API.Models.NodeVariables.Results;
using Shearlegs.Web.API.Models.Users;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<AddNodeVariableResult> AddNodeVariableAsync(AddNodeVariableParams @params)
        {
            const string sql = "dbo.AddNodeVariable";

            DynamicParameters dp = StoredProcedureParameters(@params);

            dp.Add("@NodeVariableId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            AddNodeVariableResult result = new();
            result.StoredProcedureResult = await ExecuteStoredProcedureAsync(sql, dp);
            result.NodeVariableId = dp.Get<int?>("@NodeVariableId");

            return result;
        }

        public async ValueTask<StoredProcedureResult> UpdateNodeVariableAsync(UpdateNodeVariableParams @params)
        {
            const string sql = "dbo.UpdateNodeVariable";

            return await ExecuteStoredProcedureAsync(sql, @params);
        }

        public async ValueTask<NodeVariable> GetNodeVariableAsync(GetNodeVariablesParams @params)
        {
            const string sql = "dbo.GetNodeVariables";

            NodeVariable nodeVariable = null;

            await connection.QueryAsync<NodeVariable, UserInfo, UserInfo, NodeVariable>(sql, (p, uu, cu) =>
            {
                nodeVariable = p;
                nodeVariable.UpdateUser = uu;
                nodeVariable.CreateUser = cu;

                return null;
            }, @params, commandType: CommandType.StoredProcedure);

            return nodeVariable;
        }

        public async ValueTask<IEnumerable<NodeVariable>> GetNodeVariablesAsync(GetNodeVariablesParams @params)
        {
            const string sql = "dbo.GetNodeVariables";

            return await connection.QueryAsync<NodeVariable, UserInfo, UserInfo, NodeVariable>(sql, (nv, uu, cu) =>
            {
                nv.UpdateUser = uu;
                nv.CreateUser = cu;

                return nv;
            }, @params, commandType: CommandType.StoredProcedure);
        }
    }
}
