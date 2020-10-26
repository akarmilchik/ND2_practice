using System.Collections.Generic;
using TicketsResale.Context;

namespace TicketsResale.Business.Models
{
    public class Ticket : IEntity
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public decimal Price { get; set; }

        public int SellerId { get; set; }

        //public ICollection<Order> Orders { get; set; }

        public StoreUser Seller { get; set; }

        public byte Status { get; set; }
    }
}
