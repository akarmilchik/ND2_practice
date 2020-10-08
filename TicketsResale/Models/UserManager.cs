using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketsResale.Business;
using TicketsResale.Context;

namespace TicketsResale.Models
{
    public class UserManager
    {
        private readonly IStringLocalizer<UserManager> localizer;
        private readonly ShopRepository shopRepository;
        private List<User> userStore;
        public UserManager(IStringLocalizer<UserManager> localizer, ShopRepository shopRepository)
        {
            this.localizer = localizer;
            this.shopRepository = shopRepository;
            userStore = shopRepository.GetUsersList();
        }


        public bool ValidatePassword(string userName, string password)
        {
            userStore = shopRepository.GetUsersList();
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
