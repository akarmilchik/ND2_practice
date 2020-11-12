using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models.Service
{
    public class CitiesService : ICitiesService
    {
        private readonly StoreContext context;

        public CitiesService(StoreContext storeContext)
        {
            this.context = storeContext;
        }

        public async Task<List<City>> GetCities()
        {
            return await context.Cities.ToListAsync();
        }

        public async Task<City> GetCityById(int id)
        {
            return await context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> GetCityNameById(int id)
        {
            return await context.Cities.Where(c => c.Id == id).Select(c => c.Name).FirstOrDefaultAsync();
        }


        public async Task AddCityToDb(City item)
        {
            context.Database.EnsureCreated();

            await context.Cities.AddAsync(item);

            await context.SaveChangesAsync();
        }

        public async Task UpdCityToDb(City item)
        {
            context.Database.EnsureCreated();

            context.Cities.Update(item);

            await context.SaveChangesAsync();
        }

        public async Task RemoveCityFromDb(City item)
        {
            context.Database.EnsureCreated();

            context.Cities.Remove(item);


            await context.SaveChangesAsync();
        }
    }
}
