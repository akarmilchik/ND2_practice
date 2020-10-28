using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface ITicketsService
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<EventTickets> GetEventWithTickets(int eventId);
        Task<IEnumerable<StoreUser>> GetSellers();
        Task<Ticket> GetTicketById(int id);
        Task<IEnumerable<Ticket>> GetTickets();
        Task<IEnumerable<Ticket>> GetTickets(byte status, string userName);
        Task<IEnumerable<Ticket>> GetTicketsByCart(int ticketsCartId);
    }
}
