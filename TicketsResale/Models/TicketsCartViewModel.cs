using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class TicketsCartViewModel
    {
        public ICollection<Event> Events{ get; set; }
        public ICollection<CartItem> CartItems{ get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<StoreUser> Sellers { get; set; }
    }
}
