using MySqlConnector;

namespace FlexiQueryAPI.Services
{
    public class MySqlExecutor : ISqlExecutor
    {
        private readonly string _connectionString;

        public MySqlExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<object?> ExecuteQueryAsync(string sql)
        {
            SqlSecurityValidator.Validate(sql);

            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);
            var isSelect = sql.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase);

            if (isSelect)
            {
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
            else
            {
                var affectedRows = await command.ExecuteNonQueryAsync();
                return new { affectedRows };
            }
        }
    }
}
