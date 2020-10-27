using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class TicketCreateEditModel
    {
        public int? Id { get; set; }

        public Event Event { get; set; }

        public decimal Price { get; set; }

        public byte Status { get; set; }
    }
}
