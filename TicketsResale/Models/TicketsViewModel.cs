using System.Collections.Generic;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class TicketsViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public List<StoreUser> Users { get; set; }
    }
}
