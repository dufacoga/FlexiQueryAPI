using FlexiQueryAPI.Interfaces;
using FlexiQueryAPI.Models;
using System.Text;
using System.Text.Json;

namespace FlexiQueryAPI.Utils
{
    public class QueryBuilder : IQueryBuilder
    {
        public (string Sql, object Parameters) BuildInsert(Insert dto)
        {
            var columns = string.Join(", ", dto.Values.Keys);
            var paramNames = string.Join(", ", dto.Values.Keys.Select(k => "@" + k));

            var sql = $"INSERT INTO {dto.Table} ({columns}) VALUES ({paramNames});";
            var parameters = dto.Values.ToDictionary(k => k.Key, v => ConvertJsonElement(v.Value));

            return (sql, parameters);
        }

        public (string Sql, object Parameters) BuildUpdate(Update dto)
        {
            var setClause = string.Join(", ", dto.Set.Keys.Select(k => $"{k} = @{k}"));
            var whereClause = string.Join(" AND ", dto.Where.Keys.Select(k => $"{k} = @w_{k}"));

            var sql = $"UPDATE {dto.Table} SET {setClause} WHERE {whereClause};";
            var parameters = dto.Set.ToDictionary(k => k.Key, v => ConvertJsonElement(v.Value));

            foreach (var kv in dto.Where)
                parameters["w_" + kv.Key] = ConvertJsonElement(kv.Value);

            return (sql, parameters);
        }

        public (string Sql, object Parameters) BuildDelete(Delete dto)
        {
            var whereClause = string.Join(" AND ", dto.Where.Keys.Select(k => $"{k} = @{k}"));
            var sql = $"DELETE FROM {dto.Table} WHERE {whereClause};";
            var parameters = dto.Where.ToDictionary(k => k.Key, v => ConvertJsonElement(v.Value));

            return (sql, parameters);
        }

        public (string Sql, object Parameters) BuildSelect(Select dto)
        {
            var columns = (dto.Columns != null && dto.Columns.Any())
                ? string.Join(", ", dto.Columns)
                : "*";

            var sql = new StringBuilder($"SELECT {columns} FROM {dto.Table}");
            var parameters = new Dictionary<string, object>();

            if (dto.Where != null && dto.Where.Any())
            {
                var whereClause = string.Join(" AND ", dto.Where.Keys.Select(k => $"{k} = @{k}"));
                sql.Append($" WHERE {whereClause}");
                parameters = dto.Where.ToDictionary(k => k.Key, v => ConvertJsonElement(v.Value));
            }

            sql.Append(";");
            return (sql.ToString(), parameters);
        }

        private static object? ConvertJsonElement(object? value)
        {
            if (value is JsonElement json)
            {
                return json.ValueKind switch
                {
                    JsonValueKind.String => json.GetString(),
                    JsonValueKind.Number => json.TryGetInt32(out var i) ? i :
                                            json.TryGetDouble(out var d) ? d :
                                            json.GetDecimal(),
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    JsonValueKind.Null => null,
                    _ => json.ToString()
                };
            }

            return value;
        }
    }
}