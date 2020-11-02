using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface ITicketsService
    {
        Task<EventTicketsViewModel> GetEventWithTickets(int eventId);
        Task<Ticket> GetTicketById(int id);
        Task<IEnumerable<Ticket>> GetTickets(byte ticketStatus, byte orderStatus, string userName);
        Task<IEnumerable<Ticket>> GetTicketsByCart(int ticketsCartId);
    }
}
