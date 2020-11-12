using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models.Service
{
    public class TicketsService : ITicketsService
    {
        private readonly StoreContext context;

        public TicketsService(StoreContext context)
        {
            this.context = context;
        }

        public async Task AddTicketToDb(Ticket item)
        {
            context.Database.EnsureCreated();

            await context.Tickets.AddAsync(item);

            await context.SaveChangesAsync();
        }

        public async Task UpdTicketToDb(Ticket item)
        {
            context.Database.EnsureCreated();

            context.Tickets.Update(item);

            await context.SaveChangesAsync();
        }

        public async Task<List<Ticket>> GetTickets()
        {
            return await context.Tickets.ToListAsync();
        }

        public async Task<List<Ticket>> GetTicketsByStatusesAndUserName(TicketStatuses ticketStatus, OrderStatuses orderStatus, string userName)
        {
            var chosenTickets = new List<Ticket>();
            var tickets = await context.Tickets.Include(e => e.Seller).ToListAsync();
            var sellers = await context.Users.ToListAsync();
            var orders = await context.Orders.ToListAsync();
            var seller = sellers.Where(s => s.UserName == userName).Select(s => s).FirstOrDefault();
            var otherOrdersAll = orders.Where(o => o.BuyerId != seller.Id).Select(o => o).ToList();

            for (int i = 0; i < tickets.Count; i++)
            {
                if (orderStatus != 0)
                {
                    List<Order> otherOrdersOfCurrentTicketWithNeedOrderStatus = new List<Order>();
                    if (otherOrdersAll.Count != 0)
                    {
                        otherOrdersOfCurrentTicketWithNeedOrderStatus = otherOrdersAll.Where(ci => ci.TicketId == tickets[i].Id && ci.Status == orderStatus).Select(ci => ci).ToList();
                    }

                    if ((tickets[i].SellerId == seller.Id) && (tickets[i].Status == ticketStatus) && (otherOrdersOfCurrentTicketWithNeedOrderStatus.Count != 0))
                    { chosenTickets.Add(tickets[i]); }

                }
                else 
                {
                    if ((tickets[i].SellerId == seller.Id) && (tickets[i].Status == ticketStatus))
                    { chosenTickets.Add(tickets[i]); }
                }

            }

            return chosenTickets;
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            return await context.Tickets.FindAsync(id);
        }

        public async Task<List<Ticket>> GetTicketsByUserId(string UserId)
        {

            var AllOrders = await context.Orders.Where(t => t.BuyerId == UserId).ToListAsync();

            var ordersTicketsIds = AllOrders.Select(o => o.TicketId).ToList();

            var AllTickets = await context.Tickets.Where(t => ordersTicketsIds.Contains(t.Id)) .ToListAsync();

            List<Ticket> resTickets = new List<Ticket>();


            foreach (Order order in AllOrders)
            {
                foreach (Ticket ticket in AllTickets)
                {
                    if (ticket.Id == order.TicketId)
                    {
                        resTickets.Add(ticket);
                    }
                }
            }

            return resTickets;

        }

    }
}
