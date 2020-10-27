using System.Collections.Generic;
using TicketsResale.Context;

namespace TicketsResale.Business.Models
{
    public class City : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Venue> Venues { get; set; }


    }
}
