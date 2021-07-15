using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly string _connectionString;

        public SqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<T>> GetData<T, U>(string storedProcedure, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                var data = await conn.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }

        public async Task SaveData<U>(string storedProcedure, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                await conn.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
