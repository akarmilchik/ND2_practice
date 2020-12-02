using System;

namespace TicketsResale.Queries
{
    public class EventQuery : BaseQuery
    {
        public int[] EventCategories { get; set; }
        public int[] Cities { get; set; }
        public int[] Venues { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
