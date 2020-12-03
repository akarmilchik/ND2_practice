using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;
using TicketsResale.Queries;

namespace TicketsResale.Models.Service
{
    public class VenuesService : IVenuesService
    {
        private readonly StoreContext context;
        private readonly ISortingProvider<Venue> sortingProvider;

        public VenuesService(StoreContext storeContext, ISortingProvider<Venue> sortingProvider)
        {
            this.context = storeContext;
            this.sortingProvider = sortingProvider;
        }
        public VenuesService(StoreContext storeContext)
        {
            this.context = storeContext;
        }

        public async Task AddVenueToDb(Venue item)
        {
            if (await context.Database.CanConnectAsync())
            {
                await context.Venues.AddAsync(item);

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdVenueToDb(Venue item)
        {
            if (await context.Database.CanConnectAsync())
            {
                context.Venues.Update(item);

                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveVenueFromDb(Venue item)
        {
            if (await context.Database.CanConnectAsync())
            {
                context.Venues.Remove(item);

                await context.SaveChangesAsync();
            }
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

        public async Task<IEnumerable<Venue>> GetVenuesQuery(VenueQuery query)
        {
            var queryable = context.Venues.AsQueryable();

            if (query.Cities != null)
            {
                queryable = queryable.Where(v => query.Cities.Contains(v.CityId));
            }

            return await queryable.ToListAsync();
        }
    }
}
