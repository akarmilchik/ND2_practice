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

        public User Seller { get; set; }

        public Order Order { get; set; }

        public byte Status { get; set; }
    }
}
