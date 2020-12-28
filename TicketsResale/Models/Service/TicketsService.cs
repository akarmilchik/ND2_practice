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

        public async Task<List<Ticket>> GetTicketsByStatusAndUserName(TicketStatuses ticketStatus, string userName)
        {
            List<Ticket> resultTickets = new List<Ticket>();

            string userId = await context.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstOrDefaultAsync();

            if (userId != "" && ticketStatus != 0)
            {
                resultTickets = await context.Tickets.Where(t => t.SellerId == userId && t.Status == ticketStatus).Select(t => t).ToListAsync();
            }

            return resultTickets;
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            return await context.Tickets.FindAsync(id);
        }

        public async Task<List<Ticket>> GetTicketsByUserId(string UserId)
        {
            var AllTickets = new List<Ticket>();

            if (UserId != null && UserId != "")
            {
                var ordersTicketsIds = await context.Orders.Where(o => o.BuyerId == UserId).Select(o => o.TicketId).ToListAsync();

                AllTickets = await context.Tickets.Where(t => ordersTicketsIds.Contains(t.Id)).ToListAsync();
            }

            return AllTickets;
        }

        public async Task<List<Ticket>> GetTicketsByEventId(int eventId)
        {
            var AllTickets = new List<Ticket>();

            if (eventId != 0)
            {
                AllTickets = await context.Tickets.Where(t => t.EventId == eventId).ToListAsync();
            }

            return AllTickets;
        }
    }
}
