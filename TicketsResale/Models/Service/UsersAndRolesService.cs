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

        public async Task UpdUserToDb(UsersRolesViewModel item)
        {
            context.Database.EnsureCreated();

            context.UserRoles.Update(new IdentityUserRole<string> { UserId = item.UserId, RoleId = item.Role.Id });

            await context.SaveChangesAsync();
        }

        public async Task<StoreUser> GetUserByUserName(string userName)
        {
            return await context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<List<IdentityUserRole<string>>> GetUsersRolesByUsers(List<StoreUser> users)
        {
            List<IdentityUserRole<string>> resUsersRoles = new List<IdentityUserRole<string>>();

            foreach (StoreUser user in users)
            {
                resUsersRoles.Add(await context.UserRoles.Where(ur => ur.UserId == user.Id).FirstOrDefaultAsync());
            }
            return resUsersRoles;
        }

        public async Task<List<IdentityRole>> GetRolesByUsersRoles(List<IdentityUserRole<string>> usersRoles)
        {
            List<IdentityRole> resRoles = new List<IdentityRole>();

            foreach (IdentityUserRole<string> userRole in usersRoles)
            {
                var role = await context.Roles.Where(r => r.Id == userRole.RoleId).FirstOrDefaultAsync();

                if ((resRoles.Count > 0) && !(resRoles.Contains(role)))
                {
                    resRoles.Add(role);
                }
            }

            return resRoles;
        }
    }
}
