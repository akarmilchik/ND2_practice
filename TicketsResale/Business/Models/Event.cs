using System;
using System.Globalization;

namespace TicketsResale.Business.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public Venue Venue { get; set; }

        public string Banner { get; set; }

        public string Description { get; set; }
    }
}
