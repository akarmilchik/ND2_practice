using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface IAddDataService
    {
        Task AddCityToDb(City item);
        Task AddEventToDb(Event item);
        Task AddVenueToDb(Venue item);
        Task RemoveCityFromDb(City item);
        Task RemoveEventFromDb(Event item);
        Task RemoveVenueFromDb(Venue item);
        Task UpdCityToDb(City item);
        Task UpdEventToDb(Event item);
        Task UpdUserToDb(UsersRolesViewModel item);
        Task UpdVenueToDb(Venue item);
        Task AddTicketToDb(Ticket item);
        Task UpdCartItemToDb(CartItem item);
    }
}