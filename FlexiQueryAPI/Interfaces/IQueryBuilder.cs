using FlexiQueryAPI.Models;

namespace FlexiQueryAPI.Interfaces
{
    public interface IQueryBuilder
    {
        (string Sql, object Parameters) BuildInsert(Insert dto);
        (string Sql, object Parameters) BuildUpdate(Update dto);
        (string Sql, object Parameters) BuildDelete(Delete dto);
        (string Sql, object Parameters) BuildSelect(Select dto);
    }
}
