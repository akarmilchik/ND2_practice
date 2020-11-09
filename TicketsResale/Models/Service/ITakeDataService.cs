using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface ITakeDataService
    {
        Task<List<Event>> GetEvents();
        Task<List<Venue>> GetVenues();
        Task<List<City>> GetCities();
        Task<List<StoreUser>> GetUsers();
        Task<List<IdentityRole>> GetRoles();
        Task<List<IdentityUserRole<string>>> GetUsersRoles();
        Task<List<Ticket>> GetTickets();
        Task<List<CartItem>> GetCartsItems();
        Task<City> GetCityById(int? id);
    }
}