using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class MyTicketsViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public List<Event> Events { get; set; }
        public List<StoreUser> Users { get; set; }
        public TicketStatuses ticketStatus { get; set; }
    }
}
