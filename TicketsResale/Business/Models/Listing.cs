using System.Collections.Generic;

namespace TicketsResale.Business.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public string SellerId { get; set; }
        public StoreUser Seller { get; set; }
        public decimal PricePerTicket { get; set; }
        public TicketStatuses Status { get; set; }
    }
}
