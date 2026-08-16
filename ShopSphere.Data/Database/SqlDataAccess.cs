using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopSphere.Business.Database
{
    public class SqlDataAccess
    {
        private readonly IConfiguration _configuration;
        public SqlDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string _ConnectionString => _configuration.GetConnectionString("AzureConnection");


        public async Task<IEnumerable<T>> LoadDataAsync<T, U>(
            string storedProcedure,
            U parameters,
            CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = new SqlConnection(_ConnectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand(storedProcedure, connection);
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (var prop in parameters.GetType().GetProperties())
                {
                    command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(parameters));
                }
            }
            using var reader = await command.ExecuteReaderAsync();
            var results = new List<T>();
            var properties = typeof(T).GetProperties();

            while (await reader.ReadAsync())
            {
                var item = Activator.CreateInstance<T>();
                foreach (var prop in properties)
                {
                    if (reader[prop.Name] != DBNull.Value)
                    {
                        prop.SetValue(item, reader[prop.Name]);
                    }
                }
                results.Add(item);
            }

            return results;
        }

        public async Task<int> SaveDataAsync<T>(
            string storedProcedure,
            T parameters,
            string outputParameterName = null,
            CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = new SqlConnection(_ConnectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(storedProcedure, connection);
            command.CommandType = commandType;

            foreach (var prop in parameters.GetType().GetProperties())
            {
                    command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(parameters) ?? DBNull.Value);
            }

            if (!string.IsNullOrEmpty(outputParameterName))
            {
                var outputParam = new SqlParameter($"@{outputParameterName}", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);
                 await command.ExecuteNonQueryAsync();
                 return outputParam.Value != DBNull.Value ? Convert.ToInt32(outputParam.Value) : 0;
            }
            else
            {
                return await command.ExecuteNonQueryAsync();

            }
        }

        public async Task<T> ExecuteScalarAsync<T, U>(
            string storedProcedure,
            U parameters,
            string outputParameterName = null)
        {
            using var connection = new SqlConnection(_ConnectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(storedProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;

            foreach (var prop in parameters.GetType().GetProperties())
            {
                command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(parameters));
            }
            if (!string.IsNullOrEmpty(outputParameterName))
            {
                var outputParam = new SqlParameter($"@{outputParameterName}", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);
                await command.ExecuteNonQueryAsync();
                return (T)Convert.ChangeType(outputParam.Value, typeof(T));
            }

            else
            {
                var result = await command.ExecuteScalarAsync();
                return (T)Convert.ChangeType(result, typeof(T));

            }
        }
    }
}
