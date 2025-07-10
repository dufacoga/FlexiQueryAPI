using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace FlexiQueryAPI.Services
{
    public class SqlServerExecutor : ISqlExecutor
    {
        private readonly string _connectionString;

        public SqlServerExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<object?> ExecuteQueryAsync(string sql)
        {
            SqlSecurityValidator.Validate(sql);

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand(sql, connection);
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

        public async Task<int> ExecuteNonQueryAsync(string sql)
        {
            SqlSecurityValidator.Validate(sql);

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand(sql, connection);
            return await command.ExecuteNonQueryAsync();
        }
    }
}
