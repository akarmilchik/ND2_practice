using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business;
using TicketsResale.Business.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Models
{
    public class UserManager
    {/*
        private readonly IStringLocalizer<UserManager> localizer;
        private readonly IUsersService usersService;
        private List<User> userStore;
        public UserManager(IStringLocalizer<UserManager> localizer, IUsersService usersService)
        {
            this.localizer = localizer;
            this.usersService = usersService;
        }


        public async Task<bool> ValidatePassword(string userName, string password)
        {
            //userStore = (await usersService.GetUsers()).ToList();
            /*var user = userStore.SingleOrDefault(u => u.UserName.Equals(userName));

            if (user != null)
            {
                return user.Password.Equals(password);
            }

            throw new ArgumentException(localizer["User not found"]);
        }

        /*
        public async Task<User> GetUser(string userName)
        {
            userStore = (await usersService.GetUsers()).ToList();
            var user = userStore.SingleOrDefault(u => u.UserName.ToLower().Equals(userName.ToLower()));
            return user;
        }

        public async Task<string> GetRole(string userName)
        {
            userStore = (await usersService.GetUsers()).ToList();
            var role = userStore.SingleOrDefault(u => u.UserName.Equals(userName))?.Role;

            return role;
        }
        */
    }


}
