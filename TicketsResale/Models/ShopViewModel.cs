using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class ShopViewModel
    {
        public Event[] Events { get; set; }

        public StoreUser[] Users { get; set; }

        public Ticket[] Tickets { get; set; }
        
        public City[] Cities { get; set; }

        public Venue[] Venues { get; set; }

        public CartItem[] CartItems { get; set; }
    }
}
