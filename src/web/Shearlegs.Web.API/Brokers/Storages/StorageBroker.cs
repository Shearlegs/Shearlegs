using Dapper;
using Microsoft.Extensions.Configuration;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial class StorageBroker : IStorageBroker
    {
        private readonly SqlConnection connection;

        public StorageBroker(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Default");
            connection = new SqlConnection(connectionString);
        }

        private DynamicParameters StoredProcedureParameters(dynamic parameters)
        {
            DynamicParameters p = new();
            p.AddDynamicParams(parameters);
            p.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            return p;
        }

        private int GetReturnValue(DynamicParameters p)
        {
            return p.Get<int>("@ReturnValue");
        }

        private async ValueTask<StoredProcedureResult<IEnumerable<T>>> QueryStoredProcedureAsync<T>(string procedureName, object param) 
        {
            DynamicParameters parameters = StoredProcedureParameters(param);

            IEnumerable<dynamic> resultset = await connection.QueryAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);

            StoredProcedureResult<IEnumerable<T>> result = new()
            {
                ReturnValue = GetReturnValue(parameters)
            };

            if (result.ReturnValue == 0)
            {
                result.Result = resultset.Cast<T>();
            }

            return result;
        }

        private async ValueTask<StoredProcedureResult<T>> QuerySingleStoredProcedureAsync<T>(string procedureName, object param)
        {
            DynamicParameters parameters = StoredProcedureParameters(param);

            dynamic resultset = await connection.QuerySingleOrDefaultAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);

            StoredProcedureResult<T> result = new()
            {
                ReturnValue = GetReturnValue(parameters)
            };

            if (result.ReturnValue == 0)
            {
                result.Result = (T)resultset;
            }

            return result;
        }
    }
}