using Dapper;
using DataAccessLib.Models;
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

        public async Task<int> GetScalar<U>(string storedProcedure, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                var result = await conn.ExecuteScalarAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return (Int32)result;
            }
        }

        public string GetJsonText<U>(string storedProcedure, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                var json = conn.Query<string>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return string.Concat(json);
            }
        }

        // T - outer model, V - inner model, U - parameter obj
        public List<T> GetWithNestedListData<T, V, U>(string storedProcedure, string nestedProp, U parameters)
        {
            var lookup = new Dictionary<int, T>();
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Query<T, V, T>(
                    storedProcedure,
                    (t, v) =>
                    {
                        // Временная переменная типа внешней модели
                        T ps;
                        // Id внешней модели
                        int outerModelId = (int)t.GetType().GetProperty("Id").GetValue(t, null);
                        // Проверяем нашу итоговую коллекцию. Если в ней нет ключа равного Id внешней модели
                        if (!lookup.TryGetValue(outerModelId, out ps))
                        {
                            // Добавляем ключ = ключу внейшней модели, значение - сама модель
                            lookup.Add(outerModelId, ps = t);
                        }
                        // Создаем функцию на получучение ссылки вложенной модели
                        Func<List<V>> innerModelList = () => (List<V>)ps.GetType().GetProperty(nestedProp).GetValue(ps, null);

                        if (innerModelList() is null && v is not null)
                        {
                            ps.GetType().GetProperty(nestedProp).SetValue(ps, new List<V>());
                        }

                        if (v is not null)
                        {
                            innerModelList().Add(v);
                        }

                        return ps;
                    }, parameters, commandType: CommandType.StoredProcedure);

                return lookup.Values.ToList();
            }
        }


        // T - outer model, V - inner model, U - parameter obj
        public List<T> GetWithNestedObjectData<T, V, U>(string storedProcedure, string nestedProp, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                return conn.Query<T, V, T>(
                    storedProcedure,
                    (t, v) =>
                    {
                        if (t.GetType().GetProperty(nestedProp).GetValue(t, null) is null && v is not null)
                        {
                            t.GetType().GetProperty(nestedProp).SetValue(t, v);
                        }
                        return t;
                    }, parameters, commandType: CommandType.StoredProcedure).ToList();
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
