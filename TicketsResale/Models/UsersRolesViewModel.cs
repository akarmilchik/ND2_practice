using Microsoft.AspNetCore.Identity;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class UsersRolesViewModel
    {
        public StoreUser User { get; set; }
        public IdentityRole Role { get; set; }
        public IdentityUserRole<string> UserRole { get; set; }
        public string FirstRoleId { get; set; }
        public string UserId { get; set; }
    }
}
