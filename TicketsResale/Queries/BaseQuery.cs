using TicketsResale.Business.Models;

namespace TicketsResale.Queries
{
    public abstract class BaseQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public string SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
