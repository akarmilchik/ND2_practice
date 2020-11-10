using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public interface IUsersAndRolesService
    {
        Task<IdentityRole> GetRoleByUserRole(IdentityUserRole<string> userRole);
        Task<IdentityRole> GetRoleByUserRoleId(string roleId);
        Task<List<IdentityRole>> GetRoles();
        Task<List<IdentityRole>> GetRolesByUsersRoles(List<IdentityUserRole<string>> usersRoles);
        Task<StoreUser> GetUserById(string userId);
        Task<StoreUser> GetUserByUserName(string userName);
        Task<IdentityUserRole<string>> GetUserRoleByUser(StoreUser user);
        Task<List<StoreUser>> GetUsers();
        Task<List<IdentityUserRole<string>>> GetUsersRolesByUsers(List<StoreUser> users);
        Task UpdUserToDb(UsersRolesViewModel item);
    }
}