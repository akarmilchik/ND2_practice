using Microsoft.AspNetCore.Identity;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class UsersViewModel
    {
        public StoreUser[] Users { get; set; }
        public IdentityUserRole<string>[] UsersRoles { get; set; }
        public IdentityRole[] Roles { get; set; }
    }
}
