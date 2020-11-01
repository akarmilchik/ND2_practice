using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class MyTicketsViewModel
    {
        public Ticket[] Tickets { get; set; }
        public Event[] Events { get; set; }
        public StoreUser[] Users { get; set; }
        public byte ticketStatus { get; set; }
    }
}
