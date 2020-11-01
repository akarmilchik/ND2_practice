using Microsoft.AspNetCore.Identity;
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

        public async Task AddEventToDb(Event item)
        {
            context.Database.EnsureCreated();

            await context.Events.AddAsync(item);

            await context.SaveChangesAsync();
        }

        public async Task UpdEventToDb(Event item)
        {
            context.Database.EnsureCreated();

            context.Events.Update(item);

            await context.SaveChangesAsync();
        }

        public async Task RemoveEventFromDb(Event item)
        {
            context.Database.EnsureCreated();

            context.Events.Remove(item);

            await context.SaveChangesAsync();
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

        public async Task UpdUserToDb(UsersRolesViewModel item)
        {
            context.Database.EnsureCreated();

            context.UserRoles.Update(new IdentityUserRole<string> { UserId = item.UserId, RoleId = item.Role.Id });

            await context.SaveChangesAsync();
        }


        public async Task AddTicketToDb(Ticket item)
        {
            context.Database.EnsureCreated();

            await context.Tickets.AddAsync(item);

            await context.SaveChangesAsync();
        }

        public async Task UpdCartItemToDb(CartItem item)
        {
            context.Database.EnsureCreated();

            context.CartItems.Update(item);

            await context.SaveChangesAsync();
        }

    }
}
