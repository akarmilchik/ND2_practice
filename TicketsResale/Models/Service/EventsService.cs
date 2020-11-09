using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models.Service
{
    public class EventsService : IEventsService
    {
        private readonly StoreContext context;

        public EventsService(StoreContext storeContext)
        {
            this.context = storeContext;
        }


        public async Task<Event> GetEventById(int id)
        {
            return await context.Events.FindAsync(id);
        }

        public async Task<EventTicketsViewModel> GetEventWithTickets(int eventId)
        {
            EventTicketsViewModel eventTickets = new EventTicketsViewModel();
            Dictionary<Event, List<Ticket>> dic = new Dictionary<Event, List<Ticket>>();

            var chosenEvent = await context.Events.SingleOrDefaultAsync(e => e.Id == eventId);
            var chosenTickets = await context.Tickets.Where(p => p.EventId == eventId).Select(p => p).ToListAsync();
            var chosenVenue = await context.Venues.SingleOrDefaultAsync(v => v.Id == chosenEvent.VenueId);
            var chosenCity = await context.Cities.SingleOrDefaultAsync(v => v.Id == chosenVenue.CityId);
            var sellers = await context.Users.ToListAsync();
            var cartItems = await context.CartItems.ToListAsync();

            dic.Add(chosenEvent, chosenTickets);

            eventTickets.eventTickets = dic;
            eventTickets.Venue = chosenVenue;
            eventTickets.City = chosenCity;
            eventTickets.Sellers = sellers;
            eventTickets.CartItems = cartItems;

            return eventTickets;
        }

    }
}
