using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface ITicketsService
    {
        Task<Ticket> GetTicketById(int id);
        Task<List<Ticket>> GetTicketsByStatusesAndUserName(TicketStatuses ticketStatus, CartItemStatuses orderStatus, string userName);
        Task<List<Ticket>> GetTicketsByCart(int ticketsCartId);
    }
}
