using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    interface ITicketsService
    {
        Task<Ticket> GetProductById(int id);
        Task<IEnumerable<Ticket>> GetProducts();
    }
}
