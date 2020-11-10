using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class ConfirmRejectTicketViewModel
    {
        public Ticket Ticket { get; set; }
        public Event Event { get; set; }
        public bool Confirmation { get; set; }
        public string TrackNumber { get; set; }
    }
}
