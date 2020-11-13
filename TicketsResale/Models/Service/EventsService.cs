using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
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


        public async Task AddEventToDb(Event item)
        {
            context.Database.EnsureCreated();

            await context.Events.AddAsync(item);

            await context.SaveChangesAsync();
        }

        public async Task UpdEventToDb(Event item)
        {
            context.Database.EnsureCreated();

            context.Events.Update(item);

            await context.SaveChangesAsync();
        }

        public async Task RemoveEventFromDb(Event item)
        {
            context.Database.EnsureCreated();

            context.Events.Remove(item);

            await context.SaveChangesAsync();
        }

        public async Task<List<Event>> GetEvents()
        {
            return await context.Events.ToListAsync();
        }

        public async Task<List<EventCategory>> GetEventsCategories()
        {
            return await context.EventCategories.ToListAsync();
        }

        public async Task<List<Event>> GetEventsByCategoryId(int categoryId)
        {
            return await context.Events.Where(e => e.EventCategoryId == categoryId).Select(e => e).ToListAsync();
        }

        public async Task<List<Event>> GetEventsByTickets(List<Ticket> tickets)
        {
            List<Event> resultEvents = new List<Event>();

            foreach (Ticket ticket in tickets)
            {
                var eventByTicket = await context.Events.Where(e => e.Id == ticket.EventId).FirstOrDefaultAsync();

                if (eventByTicket != null)
                {
                    resultEvents.Add(eventByTicket);
                }
            }

            return resultEvents;
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
            var orders = await context.Orders.ToListAsync();

            dic.Add(chosenEvent, chosenTickets);

            eventTickets.eventTickets = dic;
            eventTickets.Venue = chosenVenue;
            eventTickets.City = chosenCity;
            eventTickets.Sellers = sellers;
            eventTickets.Orders = orders;

            return eventTickets;
        }

        public string SaveFileAndGetName(EventCreateViewModel @event)
        {

            var fileName = $"{ Path.GetRandomFileName()}" + $".{Path.GetExtension(@event.Banner.FileName)}";

            using (var stream = System.IO.File.Create(Path.Combine("wwwroot/img/events/", fileName)))
            {
                @event.Banner.CopyTo(stream);
            }

            return fileName;
        }


    }
}
