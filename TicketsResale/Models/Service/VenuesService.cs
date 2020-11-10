using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models.Service
{
    public class VenuesService : IVenuesService
    {
        private readonly StoreContext context;

        public VenuesService(StoreContext storeContext)
        {
            this.context = storeContext;
        }

        public async Task AddVenueToDb(Venue item)
        {
            context.Database.EnsureCreated();

            await context.Venues.AddAsync(item);

            await context.SaveChangesAsync();
        }

        public async Task UpdVenueToDb(Venue item)
        {
            context.Database.EnsureCreated();

            context.Venues.Update(item);

            await context.SaveChangesAsync();
        }

        public async Task RemoveVenueFromDb(Venue item)
        {
            context.Database.EnsureCreated();

            context.Venues.Remove(item);

            await context.SaveChangesAsync();
        }

        public async Task<List<Venue>> GetVenues()
        {
            return await context.Venues.ToListAsync();
        }

        public async Task<Venue> GetVenueById(int Id)
        {
            return await context.Venues.Where(v => v.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<string> GetVenueNameById(int Id)
        {
            return await context.Venues.Where(v => v.Id == Id).Select(v => v.Name).FirstOrDefaultAsync();
        }
    }
}
