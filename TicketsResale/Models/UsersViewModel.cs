using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public class UsersViewModel
    {
        public List<StoreUser> Users { get; set; }
        public List<IdentityUserRole<string>> UsersRoles { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}
