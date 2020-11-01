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

        public async Task<IEnumerable<Ticket>> GetTickets(byte ticketStatus, byte orderStatus, string userName)
        {
           

            var chosenTickets = new List<Ticket>();
            var tickets = await context.Tickets.Include(e => e.Seller).ToListAsync();
            var sellers = await context.Users.ToListAsync();
            var cartitems = await context.CartItems.ToListAsync();
            var seller = sellers.Where(s => s.UserName == userName).Select(s => s).FirstOrDefault();
            var otherOrdersAll = cartitems.Where(ci => ci.TicketsCartId != seller.TicketsCartId).Select(ci => ci).ToList();

            for (int i = 0; i < tickets.Count; i++)
            {
                 
                //var cartItem = cartitems.Where(ci => ci.TicketId == tickets[i].Id).Select(ci => ci).FirstOrDefault();

                if (orderStatus != 0)
                {
                    List<CartItem> otherOrdersOfCurrentTicketWithNeedOrderStatus = new List<CartItem>();
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

            return chosenTickets.ToArray();
        }

        public async Task<EventTicketsViewModel> GetEventWithTickets(int eventId)
        {
            EventTicketsViewModel eventTickets = new EventTicketsViewModel();
            Dictionary<Event, List<Ticket>> dic = new Dictionary<Event, List<Ticket>>();

            var chosenEvent = await context.Events.SingleOrDefaultAsync(e => e.Id == eventId);
            var chosenTickets = await context.Tickets.Include(t => t.Event).Where(p => p.EventId == eventId).Select(p => p).ToListAsync();
            var chosenVenue = await context.Venues.SingleOrDefaultAsync(v => v.Id == chosenEvent.VenueId);
            var chosenCity = await context.Cities.SingleOrDefaultAsync(v => v.Id == chosenVenue.CityId);
            var sellers = (await context.Users.ToListAsync()).ToArray();
            var cartItems = (await context.CartItems.ToListAsync()).ToArray();

            dic.Add(chosenEvent, chosenTickets);

            eventTickets.eventTickets = dic;
            eventTickets.Venue = chosenVenue;
            eventTickets.City = chosenCity;
            eventTickets.Sellers = sellers;
            eventTickets.CartItems = cartItems;

            return eventTickets;
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            return await context.Tickets.FindAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByCart(int ticketsCartId)
        {
            var items = await context.CartItems.Where(t => t.TicketsCartId == ticketsCartId).ToListAsync();

            var AllTickets = await context.Tickets.ToListAsync();

            List<Ticket> resTickets = new List<Ticket>();


            foreach (CartItem item in items)
            {
                foreach (Ticket ticket in AllTickets)
                {
                    if (ticket.Id == item.TicketId)
                    {
                        resTickets.Add(ticket);
                    }
                }
            }

            return resTickets;

        }

    }
}
