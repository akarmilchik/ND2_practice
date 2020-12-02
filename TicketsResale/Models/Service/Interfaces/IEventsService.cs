using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Queries;

namespace TicketsResale.Models.Service
{
    public interface IEventsService
    {
        Task AddEventToDb(Event item);
        Task<Event> GetEventById(int id);
        Task<IEnumerable<EventCategory>> GetEventCategoriesWithEvents();
        Task<List<Event>> GetEvents();
        Task<List<Event>> GetEventsByCategoryId(int categoryId);
        Task<List<Event>> GetEventsByTickets(List<Ticket> tickets);
        Task<List<EventCategory>> GetEventsCategories();
        Task<PagedResult<Event>> GetEventsQuery(EventQuery query);
        Task<IEnumerable<Event>> GetMatchedEvents(EventQuery query, int matchedEventsCount);
        Task RemoveEventFromDb(Event item);
        string SaveFileAndGetName(EventCreateViewModel @event);
        Task UpdEventToDb(Event item);
    }
}
