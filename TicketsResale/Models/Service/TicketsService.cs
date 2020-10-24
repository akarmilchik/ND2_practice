using Microsoft.EntityFrameworkCore;
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

            dic.Add(chosenEvent, chosenTickets);

            eventTickets.eventTickets = dic;

            return eventTickets;
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            return await context.Tickets.FindAsync(id);
        }

    }
}
