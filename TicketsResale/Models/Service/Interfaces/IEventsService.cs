using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface IEventsService
    {
        Task AddEventToDb(Event item);
        Task<Event> GetEventById(int id);
        Task<List<Event>> GetEvents();
        Task<List<Event>> GetEventsByCategoryId(int categoryId);
        Task<List<Event>> GetEventsByTickets(List<Ticket> tickets);
        Task<List<EventCategory>> GetEventsCategories();
        Task<EventTicketsViewModel> GetEventWithTickets(int eventId);
        Task RemoveEventFromDb(Event item);
        string SaveFileAndGetName(EventCreateViewModel @event);
        Task UpdEventToDb(Event item);
    }
}
