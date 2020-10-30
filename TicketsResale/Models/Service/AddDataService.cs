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
