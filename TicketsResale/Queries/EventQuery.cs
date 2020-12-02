using System;

namespace TicketsResale.Queries
{
    public class EventQuery : BaseQuery
    {
        public int[] EventCategories { get; set; }
        public int[] Cities { get; set; }
        public int[] Venues { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
