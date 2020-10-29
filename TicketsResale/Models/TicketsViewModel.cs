using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class TicketsViewModel
    {
        public Ticket[] Tickets { get; set; }
        public StoreUser[] Users { get; set; }
    }
}
