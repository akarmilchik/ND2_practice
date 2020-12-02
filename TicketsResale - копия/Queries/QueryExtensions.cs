using System.Linq;

namespace TicketsResale.Queries
{
    public static class QueryExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> queryable, BaseQuery query)
        {
            if (query.PageSize <= 0)
                query.PageSize = 4;

            if (query.Page <= 0)
                query.Page = 1;

            return queryable.Skip(query.PageSize * (query.Page - 1)).Take(query.PageSize);
        }

    }
}
