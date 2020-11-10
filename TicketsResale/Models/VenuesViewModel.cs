using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class VenuesViewModel
    {
        public List<Venue> Venues { get; set; }
        public List<City> Cities { get; set; }
    }
}
