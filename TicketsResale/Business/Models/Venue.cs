using System.Collections.Generic;
using TicketsResale.Context;

namespace TicketsResale.Business.Models
{
    public class Venue : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public City City { get; set; }

        public int CityId { get; set; }
    }
}
