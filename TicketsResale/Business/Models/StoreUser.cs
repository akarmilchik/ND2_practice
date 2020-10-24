using Microsoft.AspNetCore.Identity;

namespace TicketsResale.Business.Models
{
    public class StoreUser : IdentityUser
    {
        public int TicketsCartId { get; set; }
        public TicketsCart TicketsCart { get; set; }

    }
}
