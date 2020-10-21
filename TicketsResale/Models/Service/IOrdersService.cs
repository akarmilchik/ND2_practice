using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface IOrdersService
    {
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrders();
    }
}
