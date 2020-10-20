using TicketsResale.Context;

namespace TicketsResale.Business.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public byte Status { get; set; }

        public int BuyerId { get; set; }

        public User Buyer { get; set; }

        public string TrackNumber { get; set; }

    }
}
