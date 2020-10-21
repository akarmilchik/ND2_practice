﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models.Service
{
    public class UsersService : IUsersService
    {
        private readonly StoreContext context;

        public UsersService(StoreContext context)
        {
            this.context = context;
        }


        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }


        public async Task<User> GetUserById(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByName(string name)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.UserName == name);
        }
    }
}
