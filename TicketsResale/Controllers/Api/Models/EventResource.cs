using System;

namespace TicketsResale.Controllers.Api.Models
{
    public class EventResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string VenueName { get; set; }
        public string CityName { get; set; }
        public int EventCategoryId { get; set; }
        public string Banner { get; set; }
        public string Description { get; set; }
    }
}
