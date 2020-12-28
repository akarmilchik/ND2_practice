using System;
using System.Collections.Generic;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class EventsViewModel
    {
        public IEnumerable<EventCategory> EventsCategories { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Venue> Venues { get; set; }
        public string searchString { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string SearchString { get; set; }
        public SortParams SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
        public int PageSize { get; set; }
        
    }
}
