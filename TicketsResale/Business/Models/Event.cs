using System;
using TicketsResale.Context;

namespace TicketsResale.Business.Models
{
    public class Event : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int VenueId { get; set; }

        public Venue Venue { get; set; }

        public string Banner { get; set; }

        public string Description { get; set; }
    }
}
