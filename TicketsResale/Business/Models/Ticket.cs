using System.Collections.Generic;
using System.ComponentModel;
using TicketsResale.Context;

namespace TicketsResale.Business.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public int SellerId { get; set; }

        public StoreUser Seller { get; set; }

        public decimal Price { get; set; }

        public byte Status { get; set; }
    }
}
