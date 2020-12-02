using System;
using System.ComponentModel.DataAnnotations;
using TicketsResale.Context;

namespace TicketsResale.Business.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public int VenueId { get; set; }

        public Venue Venue { get; set; }

        public int EventCategoryId { get; set; }

        public EventCategory EventCategory { get; set; }

        public string Banner { get; set; }

        public string Description { get; set; }
    }
}
