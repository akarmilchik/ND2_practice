using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsResale.Queries
{
    public class PagedResult<T>
    {       
        public int TotalCount { get; set; }
        public ICollection<T> Items { get; set; }


    }
}
