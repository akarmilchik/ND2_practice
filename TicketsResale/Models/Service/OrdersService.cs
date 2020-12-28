using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;
using TicketsResale.Controllers.Api.Models.Requests;

namespace TicketsResale.Models.Service
{
    public class OrdersService : IOrdersService
    {
        private readonly StoreContext context;

        public OrdersService(StoreContext context)
        {
            this.context = context;
        }


        public async Task<List<Order>> GetOrders()
        {
            if (await context.Database.CanConnectAsync())
            {
                return await context.Orders.ToListAsync();
            }
            else return null;
        }

        public async Task<List<Order>> GetOrdersByTicketId(int ticketId)
        {
            if (await context.Database.CanConnectAsync())
            {
                return await context.Orders.Where(o => o.TicketId == ticketId).ToListAsync();
            }
            else return null;
        }

        public async Task<List<Order>> GetOrdersByUserName(string userName)
        {
            if (userName != null && userName != "")
            {
                var user = await context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();

                if (await context.Database.CanConnectAsync() && user != null)
                {
                    return await context.Orders.Where(o => o.BuyerId == user.Id).ToListAsync();
                }
                else return null;
            }
            else return null;
        }

        public async Task AddTicketToOrder(string userName, Ticket item)
        {
            if (userName != null && userName != "")
            {
                var user = await context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();

                if (await context.Database.CanConnectAsync())
                {
                    await context.Orders.AddAsync(new Order { Buyer = user, Ticket = item, Status = OrderStatuses.waiting, TrackNumber = "" });
                }
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdOrderToDb(Order item)
        {
            if (await context.Database.CanConnectAsync())
            {
                context.Orders.Update(item);
            }
            await context.SaveChangesAsync();
        }

    }
}
