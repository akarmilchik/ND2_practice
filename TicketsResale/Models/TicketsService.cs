using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models
{
    public class TicketsService : ITicketsService
    {
        private readonly StoreContext context;


        public TicketsService(StoreContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Ticket>> GetProducts()
        {
            return await context.Tickets.ToListAsync();
        }

        public async Task<Ticket> GetProductById(int id)
        {
            return await context.Tickets.FindAsync(id);
        }

    }
}
