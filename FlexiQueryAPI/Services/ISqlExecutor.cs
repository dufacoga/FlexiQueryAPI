namespace FlexiQueryAPI.Services
{
    public interface ISqlExecutor
    {
        Task<object?> ExecuteQueryAsync(string sql);
    }
}
