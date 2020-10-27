using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsResale.Business.Models
{
    public class StoreUser : IdentityUser
    {
        public int TicketsCartId { get; set; }

        public TicketsCart TicketsCart { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Localization { get; set; }

    }
}
