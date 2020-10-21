using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketsResale.Business;
using TicketsResale.Business.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Models
{
    public class UserManager
    {
        private readonly IStringLocalizer<UserManager> localizer;
        private readonly IUsersService usersService;
        private List<User> userStore;
        public UserManager(IStringLocalizer<UserManager> localizer, IUsersService usersService)
        {
            this.localizer = localizer;
            this.usersService = usersService;
            userStore =  this.usersService.GetUsers().ToList();
        }


        public bool ValidatePassword(string userName, string password)
        {
            userStore = usersService.GetUsers().ToList();
            var user = userStore.SingleOrDefault(u => u.UserName.Equals(userName));

            if (user != null)
            {
                return user.Password.Equals(password);
            }

            throw new ArgumentException(localizer["User not found"]);
        }


        public User GetUser(string userName) => userStore
    .SingleOrDefault(u => u.UserName.ToLower().Equals(userName.ToLower()));

        public string GetRole(string userName) => userStore.SingleOrDefault(u => u.UserName.Equals(userName))?.Role;

    }


}
