using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await context.Tickets.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await context.Events.ToListAsync();
        }

        public async Task<IEnumerable<StoreUser>> GetSellers()
        {
            return await context.Users.ToListAsync();
        }



        public async Task<IEnumerable<Ticket>> GetTickets(byte status, string userName)
        {
            var chosenTickets = new List<Ticket>();
            var tickets = await context.Tickets.ToListAsync();

            for (int i = 0; i < tickets.Count; i++)
            {
                if ((tickets[i].Status == status)/* && (tickets[i].Seller.UserName == userName)*/)
                    chosenTickets.Add(tickets[i]);
            }

            return chosenTickets.ToArray();
        }

        public async Task<EventTickets> GetEventWithTickets(int eventId)
        {
            EventTickets eventTickets = new EventTickets();
            Dictionary<Event, List<Ticket>> dic = new Dictionary<Event, List<Ticket>>();

            var chosenEvent = await context.Events.SingleOrDefaultAsync(e => e.Id == eventId);
            var chosenTickets = await context.Tickets.Include(t => t.Event).Where(p => p.EventId == eventId).Select(p => p).ToListAsync();
            var chosenVenue = await context.Venues.SingleOrDefaultAsync(v => v.Id == chosenEvent.VenueId);
            var chosenCity = await context.Cities.SingleOrDefaultAsync(v => v.Id == chosenVenue.CityId);
            dic.Add(chosenEvent, chosenTickets);

            eventTickets.eventTickets = dic;
            eventTickets.Venue = chosenVenue;
            eventTickets.City = chosenCity;

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
