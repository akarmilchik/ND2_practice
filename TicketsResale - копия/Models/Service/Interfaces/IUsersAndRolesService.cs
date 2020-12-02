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
        Task<string> GetUserAddressByUserName(string userName);
        Task<string> GetUserFirstNameByUserName(string userName);
        Task<string> GetUserLastNameByUserName(string userName);
        Task<Localizations> GetUserLocalizationByUserName(string userName);
        Task<StoreUser> GetUserById(string userId);
        Task<StoreUser> GetUserDataAsync(StoreUser user);
        Task<IdentityUserRole<string>> GetUserRoleByUser(StoreUser user);
        Task<List<StoreUser>> GetUsers();
        Task<List<IdentityUserRole<string>>> GetUsersRolesByUsers(List<StoreUser> users);
        Task UpdUserAddress(StoreUser user, string address);
        Task UpdUserFirstName(StoreUser user, string firstName);
        Task UpdUserLastName(StoreUser user, string lastName);
        Task UpdUserLocalization(StoreUser user, Localizations localization);
        Task<StoreUser> GetUserByUserName(string userName);
        Task<List<StoreUser>> GetUsersByListOfId(List<string> userIds);
    }
}