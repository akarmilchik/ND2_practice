using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface ITicketsService
    {
        Task<EventTickets> GetEventWithTickets(int eventId);
        Task<Ticket> GetTicketById(int id);
        Task<IEnumerable<Ticket>> GetTickets();
        Task<IEnumerable<Ticket>> GetTickets(byte status, string userName);
    }
}
