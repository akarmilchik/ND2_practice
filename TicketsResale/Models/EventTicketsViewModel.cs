using System.Collections.Generic;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class EventTicketsViewModel
    {
        public Dictionary<Event, List<Ticket>> eventTickets { get; set; }

        public StoreUser[] Sellers { get; set; }

        public CartItem[] CartItems { get; set; }

        public Event Event { get; set; }

        public Venue Venue {get; set;}

        public City City { get; set; }

    }
}
