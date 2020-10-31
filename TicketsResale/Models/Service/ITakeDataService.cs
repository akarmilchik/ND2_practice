using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface ITakeDataService
    {
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<Event>> GetEvents();
        Task<IEnumerable<Ticket>> GetTickets();
        Task<IEnumerable<StoreUser>> GetUsers();
        Task<IEnumerable<IdentityRole>> GetRoles();
        Task<IEnumerable<IdentityUserRole<string>>> GetUsersRoles();
        Task<IEnumerable<Venue>> GetVenues();
    }
}