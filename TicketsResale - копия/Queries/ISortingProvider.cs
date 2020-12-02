using System.Linq;

namespace TicketsResale.Queries
{
    public interface ISortingProvider<T>
    {
        IOrderedQueryable<T> ApplySorting(IQueryable<T> queryable, BaseQuery query);
    }
}