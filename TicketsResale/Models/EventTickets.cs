using System.Collections.Generic;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class EventTickets
    {
        public Dictionary<Event, List<Ticket>> eventTickets { get; set; }

    }
}
