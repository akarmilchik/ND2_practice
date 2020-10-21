using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models.Service
{
    public class OrdersService : IOrdersService
    {
        private readonly StoreContext context;

        public OrdersService(StoreContext storeContext)
        {
            this.context = storeContext;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await context.Orders.FindAsync(id);
        }

    }
}
