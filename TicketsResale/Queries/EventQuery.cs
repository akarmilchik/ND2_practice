using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Queries
{
    public class EventQuery : BaseQuery
    {
        public int[] EventCategories { get; set; }
    }
}
