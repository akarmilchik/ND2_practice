using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models
{
    public interface ITicketsCartService
    {
        Task AddItemToCart(int id, Ticket item, int count);
        Task ChangeItemCount(int id, Ticket item, int newCount);
        Task ClearCart(int id);
        Task<int> CreateCart();
        Task<TicketsCart> FindCart(int id);
        Task RemoveItemFromCart(int id, Ticket item);
    }
}