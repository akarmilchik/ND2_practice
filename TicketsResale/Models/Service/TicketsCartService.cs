using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models.Service
{
    public class TicketsCartService : ITicketsCartService
    {
        private readonly StoreContext context;

        public TicketsCartService(StoreContext context)
        {
            this.context = context;
        }

        public async Task<TicketsCart> FindCart(int id)
        {
            var cart = await GetCart(id);
            return cart;
        }

        public async Task AddItemToCart(int id, Ticket item, int count)
        {
            if (count <= 0) throw new ArgumentException(nameof(count));

            var cart = await GetCart(id);
            if (cart.CartItems.Any(ci => ci.TicketId == item.Id))
                cart.CartItems.Single(ci => ci.TicketId == item.Id).Count += count;
            else
                cart.CartItems.Add(new CartItem { TicketId = item.Id, Count = count });
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        private async Task<TicketsCart> GetCart(int id)
        {
            var cart = await context.TicketsCarts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Ticket).ThenInclude(e => e.Event)
                .SingleOrDefaultAsync(c => c.Id == id);
            return cart;
        }

        public async Task<int> GetTicketsCartIdByUserId(string id)
        {
            return context.Users.Where(u => u.Id == id).Select(u => u.TicketsCartId).FirstOrDefault();
        }

        public async Task ChangeItemCount(int id, Ticket item, int newCount)
        {
            var cart = await GetCart(id);
            if (cart.CartItems.Any(ci => ci.TicketId == item.Id))
                cart.CartItems.Single(ci => ci.TicketId == item.Id).Count = newCount;
            else
                throw new ArgumentException(nameof(item));
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCart(int id, Ticket item)
        {
            var cart = await GetCart(id);
            var cartItem = cart.CartItems.SingleOrDefault(ci => ci.TicketId == item.Id);
            if (cartItem == null) return;
            cart.CartItems.Remove(cartItem);
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        public async Task ClearCart(int id)
        {
            var cart = await GetCart(id);
            cart.CartItems.Clear();
            context.Update(cart);
            await context.SaveChangesAsync();
        }

        public async Task<int> CreateCart()
        {
            var newCart = await context.TicketsCarts.AddAsync(new TicketsCart());
            await context.SaveChangesAsync();
            return newCart.Entity.Id;
        }
        
    }
}
