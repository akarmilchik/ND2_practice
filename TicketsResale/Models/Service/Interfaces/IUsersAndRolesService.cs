using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public interface IUsersAndRolesService
    {
        Task<List<IdentityRole>> GetRolesByUsersRoles(List<IdentityUserRole<string>> usersRoles);
        Task<StoreUser> GetUserByUserName(string userName);
        Task<List<StoreUser>> GetUsers();
        Task<List<IdentityUserRole<string>>> GetUsersRolesByUsers(List<StoreUser> users);
        Task UpdUserToDb(UsersRolesViewModel item);
    }
}