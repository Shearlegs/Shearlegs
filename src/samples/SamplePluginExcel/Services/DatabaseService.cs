using Dapper;
using Microsoft.Extensions.DependencyInjection;
using SamplePluginExcel.Models;
using Shearlegs.API.Plugins.Attributes;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SamplePluginExcel.Services
{
    [Service(Lifetime = ServiceLifetime.Singleton)]
    public class DatabaseService
    {
        private readonly SqlConnection connection;

        public DatabaseService(SampleExcelParameters parameters)
        {
            connection = new SqlConnection(parameters.ConnectionString);
        }

        public async Task<IEnumerable<InformationSchemaTable>> GetTablesAsync()
        {
            const string sql = "SELECT * FROM INFORMATION_SCHEMA.TABLES;";

            return await connection.QueryAsync<InformationSchemaTable>(sql);
        }
    }
}
