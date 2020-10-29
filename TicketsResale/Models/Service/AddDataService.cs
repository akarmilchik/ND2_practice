using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models.Service
{
    public class AddDataService : IAddDataService
    {
        private readonly StoreContext context;

        public AddDataService(StoreContext context)
        {
            this.context = context;     
        }

        
        public async Task AddCityToDb (City item)
        {
            City city = new City { Name = item.Name };
            context.Database.EnsureCreated();
            await context.Cities.AddAsync(city);
            await context.SaveChangesAsync();
        }

        public async Task RemoveCityToDb(City item)
        {
            City city = new City { Name = item.Name };
            context.Database.EnsureCreated();
            context.Cities.Remove(city);
            await context.SaveChangesAsync();
        }

    }
}
