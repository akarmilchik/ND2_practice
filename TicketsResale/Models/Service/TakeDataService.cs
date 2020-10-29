using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models.Service
{
    public class TakeDataService : ITakeDataService
    {
        private readonly StoreContext context;

        public TakeDataService(StoreContext storeContext)
        {
            this.context = storeContext;
        }
        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await context.Events.ToListAsync();
        }
        public async Task<IEnumerable<Venue>> GetVenues()
        {
            return await context.Venues.ToListAsync();
        }
        public async Task<IEnumerable<City>> GetCities()
        {
            return await context.Cities.ToListAsync();
        }
        public async Task<IEnumerable<StoreUser>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }
        public async Task<IEnumerable<IdentityRole>> GetUsersRoles()
        {
            return await context.Roles.ToListAsync();
        }
        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await context.Tickets.ToListAsync();
        }
    }
}
