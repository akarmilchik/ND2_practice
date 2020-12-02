using Microsoft.AspNetCore.Identity;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class UsersRolesViewModel
    {
        public string UserId { get; set; }
        public StoreUser User { get; set; }
        public string FirstRoleId { get; set; }
        public IdentityRole Role { get; set; }
        public IdentityUserRole<string> UserRole { get; set; }


    }
}
