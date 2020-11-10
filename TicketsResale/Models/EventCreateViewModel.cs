using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class EventCreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime Date { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
        public string Description { get; set; }
        public IFormFile Banner { get; set; }
    }
}
