using Microsoft.Data.SqlClient;

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

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);
            var isSelect = sql.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase);

            if (isSelect)
            {
                using var reader = await command.ExecuteReaderAsync();
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
            else
            {
                var affectedRows = await command.ExecuteNonQueryAsync();
                return new { affectedRows };
            }
        }
    }
}
