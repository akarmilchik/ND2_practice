using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Event>> GetEvents()
        {
            return await context.Events.ToListAsync();
        }
        public async Task<List<Venue>> GetVenues()
        {
            return await context.Venues.ToListAsync();
        }
        public async Task<List<City>> GetCities()
        {
            return await context.Cities.ToListAsync();
        }

        public async Task<City> GetCityById(int? id)
        {
            return await context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<StoreUser>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }
        public async Task<List<IdentityRole>> GetRoles()
        {
            return await context.Roles.ToListAsync();
        }
        public async Task<List<IdentityUserRole<string>>> GetUsersRoles()
        {
            return await context.UserRoles.ToListAsync();
        }
        public async Task<List<Ticket>> GetTickets()
        {
            return await context.Tickets.ToListAsync();
        }

        public async Task<List<CartItem>> GetCartsItems()
        {
            return await context.CartItems.ToListAsync();
        }
    }
}
