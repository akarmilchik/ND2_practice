using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class EventsViewModel
    {
        public Event[] Events { get; set; }
        public City[] Cities { get; set; }
        public Venue[] Venues { get; set; }
    }
}
