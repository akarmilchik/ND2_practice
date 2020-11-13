using System.Collections.Generic;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class EventsViewModel
    {
        public List<EventCategory> EventsCategories { get; set; }
        public List<Event> Events { get; set; }
        public List<City> Cities { get; set; }
        public List<Venue> Venues { get; set; }
    }
}
