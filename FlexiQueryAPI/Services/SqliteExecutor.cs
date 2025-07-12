using FlexiQueryAPI.Interfaces;
using Microsoft.Data.Sqlite;

namespace FlexiQueryAPI.Services
{
    public class SqliteExecutor : ISqlExecutor
    {
        private readonly string _connectionString;

        public SqliteExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<object?> ExecuteQueryAsync(string sql, object parameters)
        {
            SqlSecurityValidator.Validate(sql);

            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqliteCommand(sql, connection);
            AddParameters(command, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            var result = new List<Dictionary<string, object>>();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null! : reader.GetValue(i);
                }

                result.Add(row);
            }

            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(string sql, object parameters)
        {
            SqlSecurityValidator.Validate(sql);

            await using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqliteCommand(sql, connection);
            AddParameters(command, parameters);
            return await command.ExecuteNonQueryAsync();
        }

        private static void AddParameters(SqliteCommand command, object parameters)
        {
            var dictionary = GetParameterDictionary(parameters);

            foreach (var param in dictionary)
            {
                command.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
            }
        }

        private static Dictionary<string, object> GetParameterDictionary(object parameters)
        {
            if (parameters is Dictionary<string, object> dict)
                return dict;

            return parameters
                .GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(parameters) ?? DBNull.Value);
        }
    }
}