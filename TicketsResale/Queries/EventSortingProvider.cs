using System;
using System.Linq.Expressions;
using TicketsResale.Business.Models;

namespace TicketsResale.Queries
{
    public class EventSortingProvider : BaseSortingProvider<Event>
    {
        protected override Expression<Func<Event, object>> GetSortExpression(BaseQuery query)
        {
            return query.SortBy switch
            {
                "Name" => e => e.Name,
                "Date" => e => e.Date,
                _ => e => e.Id,

            };
        }
    }
}
