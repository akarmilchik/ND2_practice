using TicketsResale.Business.Models;

namespace TicketsResale.Controllers.Api.Models
{
    public class ListingResource
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public TicketStatuses Status { get; set; }
        public decimal PricePerTicket { get; set; }
    }
}
