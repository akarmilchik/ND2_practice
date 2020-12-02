using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface IOrdersService
    { 
        Task AddTicketToOrder(string userName, Ticket item);
        Task<List<Order>> GetOrders();
        Task<List<Order>> GetOrdersByTicketId(int ticketId);
        Task<List<Order>> GetOrdersByUserName(string userName);
        Task UpdOrderToDb(Order item);
    }
}