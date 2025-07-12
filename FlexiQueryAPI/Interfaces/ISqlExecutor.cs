namespace FlexiQueryAPI.Interfaces
{
    public interface ISqlExecutor
    {
        Task<object?> ExecuteQueryAsync(string sql, object parameters);
        Task<int> ExecuteNonQueryAsync(string sql, object parameters);
    }
}
