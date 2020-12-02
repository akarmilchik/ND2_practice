using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models
{
    public class UsersAndRolesService : IUsersAndRolesService
    {
        private readonly StoreContext context;

        public UsersAndRolesService(StoreContext context)
        {
            this.context = context;
        }

        public async Task<List<StoreUser>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await context.Roles.ToListAsync();
        }

        public async Task UpdUserFirstName(StoreUser user, string firstName)
        {
            if (await context.Database.CanConnectAsync() && firstName != null && firstName != "" && user != null)
            {

                user.FirstName = firstName;
                context.Users.Update(user);

                await context.SaveChangesAsync();
            }
        }
        public async Task UpdUserLastName(StoreUser user, string lastName)
        {
            if (await context.Database.CanConnectAsync() && lastName != null && lastName != "" && user != null)
            {
                user.LastName = lastName;
                context.Users.Update(user);

                await context.SaveChangesAsync();
            }
        }
        public async Task UpdUserAddress(StoreUser user, string address)
        {
            if (await context.Database.CanConnectAsync() && address != null && address != "" && user != null)
            {
                user.Address = address;
                context.Users.Update(user);

                await context.SaveChangesAsync();
            }
        }
        public async Task UpdUserLocalization(StoreUser user, Localizations localization)
        {
            if (await context.Database.CanConnectAsync() && localization != 0 && user != null)
            {
                user.Localization = localization;
                context.Users.Update(user);

                await context.SaveChangesAsync();
            }
        }

        public async Task<string> GetUserFirstNameByUserName(string userName)
        {
            return await context.Users.Where(u => u.UserName == userName).Select(u => u.FirstName).FirstOrDefaultAsync();
        }

        public async Task<string> GetUserLastNameByUserName(string userName)
        {
            return await context.Users.Where(u => u.UserName == userName).Select(u => u.LastName).FirstOrDefaultAsync();
        }


        public async Task<string> GetUserAddressByUserName(string userName)
        {
            return await context.Users.Where(u => u.UserName == userName).Select(u => u.Address).FirstOrDefaultAsync();
        }

        public async Task<Localizations> GetUserLocalizationByUserName(string userName)
        {
            return await context.Users.Where(u => u.UserName == userName).Select(u => u.Localization).FirstOrDefaultAsync();
        }


        public async Task<IdentityUserRole<string>> GetUserRoleByUser(StoreUser user)
        {
            return await context.UserRoles.Where(u => u.UserId == user.Id).FirstOrDefaultAsync();
        }

        public async Task<StoreUser> GetUserById(string userId)
        {
            return await context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<List<StoreUser>> GetUsersByListOfId(List<string> userIds)
        {
            return await context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
        }

        public async Task<StoreUser> GetUserByUserName(string userName)
        {
            return await context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<List<IdentityUserRole<string>>> GetUsersRolesByUsers(List<StoreUser> users)
        {
            List<IdentityUserRole<string>> resUsersRoles = new List<IdentityUserRole<string>>();
            if (users != null)
            {
                foreach (StoreUser user in users)
                {
                    resUsersRoles.Add(await context.UserRoles.Where(ur => ur.UserId == user.Id).FirstOrDefaultAsync());
                }
            }
            return resUsersRoles;
        }

        public async Task<List<IdentityRole>> GetRolesByUsersRoles(List<IdentityUserRole<string>> usersRoles)
        {
            List<IdentityRole> resRoles = new List<IdentityRole>();
            if (usersRoles != null)
            {
                foreach (IdentityUserRole<string> userRole in usersRoles)
                {
                    var role = await context.Roles.Where(r => r.Id == userRole.RoleId).FirstOrDefaultAsync();

                    if (!(resRoles.Contains(role)))
                    {
                        resRoles.Add(role);
                    }
                }
            }
            return resRoles;
        }

        public async Task<IdentityRole> GetRoleByUserRole(IdentityUserRole<string> userRole)
        {
             return await context.Roles.Where(r => r.Id == userRole.RoleId).FirstOrDefaultAsync();
        }

        public async Task<IdentityRole> GetRoleByUserRoleId(string roleId)
        {
            return await context.Roles.Where(r => r.Id == roleId).FirstOrDefaultAsync();
        }


        public async Task<StoreUser> GetUserDataAsync(StoreUser user)
        {
            return await context.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();
        }

    }
}
