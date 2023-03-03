using Dapper;
using Microsoft.Extensions.Configuration;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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
            int? returnValue = p.Get<int?>("@ReturnValue");

            return  returnValue.HasValue ? returnValue.Value : 0;
        }


        private async ValueTask<StoredProcedureResult<List<T>>> QueryStoredProcedureAsync<T>(string procedureName, object param) 
        {
            DynamicParameters parameters = StoredProcedureParameters(param);

            DbDataReader reader = await connection.ExecuteReaderAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);

            StoredProcedureResult<List<T>> result = new()
            {
                ReturnValue = GetReturnValue(parameters),
                Result = new List<T>()
            };

            if (result.ReturnValue == 0)
            {
                Func<IDataReader, T> parser = reader.GetRowParser<T>(typeof(T));
                    
                while (await reader.ReadAsync())
                {
                    T obj = parser(reader);
                    result.Result.Add(obj);
                }
            }

            return result;
        }

        private async ValueTask<StoredProcedureResult<T>> QuerySingleStoredProcedureAsync<T>(string procedureName, object param)
        {
            DynamicParameters parameters = StoredProcedureParameters(param);

            DbDataReader reader = await connection.ExecuteReaderAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);

            StoredProcedureResult<T> result = new()
            {
                ReturnValue = GetReturnValue(parameters)
            };

            if (result.ReturnValue == 0)
            {
                Func<IDataReader, T> parser = reader.GetRowParser<T>(typeof(T));

                while (await reader.ReadAsync())
                {
                    result.Result = parser(reader);
                }                
            }

            return result;
        }
    }
}