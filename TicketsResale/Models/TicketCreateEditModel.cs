using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class TicketCreateEditModel
    {
        public int? Id { get; set; }

        public Event Event { get; set; }

        public decimal Price { get; set; }

        public TicketStatuses Status { get; set; } = TicketStatuses.selling;

        public string SellerName { get; set; }

        public StoreUser Seller { get; set; }
    }
}
