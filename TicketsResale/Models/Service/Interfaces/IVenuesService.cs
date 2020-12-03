using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Queries;

namespace TicketsResale.Models.Service
{
    public interface IVenuesService
    {
        Task AddVenueToDb(Venue item);
        Task<Venue> GetVenueById(int Id);
        Task<string> GetVenueNameById(int Id);
        Task<List<Venue>> GetVenues();
        Task<IEnumerable<Venue>> GetVenuesQuery(VenueQuery query);
        Task RemoveVenueFromDb(Venue item);
        Task UpdVenueToDb(Venue item);
    }
}