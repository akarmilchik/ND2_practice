using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Context
{
    public class Ticket
    {
        public int Id { get; set; }

        public Event Event { get; set; }

        public decimal Price { get; set; }

        public User Seller { get; set; }

        public TicketStatuses Status { get; set; }
    }
}
