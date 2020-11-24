using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;
using TicketsResale.Queries;

namespace TicketsResale.Models.Service
{
    public class EventsService : IEventsService
    {
        private readonly StoreContext context;
        private readonly ISortingProvider<Event> sortingProvider;

        public EventsService(StoreContext storeContext, ISortingProvider<Event> sortingProvider)
        {
            this.context = storeContext;
            this.sortingProvider = sortingProvider;
        }
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

        public async Task<PagedResult<Event>> GetEventsQuery(EventQuery query)
        {
            var queryable = context.Events.AsQueryable();

            if (query.EventCategories != null)
            {
                queryable = queryable.Where(e => query.EventCategories.Contains(e.EventCategoryId));
            }

            var count = await queryable.CountAsync();

            queryable = sortingProvider.ApplySorting(queryable, query);

            queryable = queryable.ApplyPagination(query);

            var items = await queryable.ToListAsync();

            return new PagedResult<Event> { TotalCount = count, Items = items };
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


        public async Task<IEnumerable<EventCategory>> GetEventCategoriesWithEvents()
        {
            var categories = context.EventCategories.Where(c => c.Events.Any());

            foreach (EventCategory category in categories)
            {
                category.Events = await context.Events.Where(e => e.EventCategoryId == category.Id).Select(e => e).ToListAsync();
            }

            return await categories.ToListAsync();
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
