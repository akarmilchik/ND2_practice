using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    public class CartController : Controller
    {
        private const string CartCookieName = "TicketsResale.TicketsCartId";
        private readonly ITicketsCartService ticketsCartService;
        private readonly ITicketsService ticketsService;

        public CartController(ITicketsCartService ticketsCartService, ITicketsService ticketsService)
        {
            this.ticketsCartService = ticketsCartService;
            this.ticketsService = ticketsService;
        }

        public async Task<IActionResult> Index()
        {
            var cartId = await GetSetCartId();
            var cart = await ticketsCartService.FindCart(cartId);
            var items = cart.CartItems.Select(ci => new TicketsCartViewModel
            {
                TicketId = ci.TicketId,
                TicketName = ci.Ticket.Event.Name,
                Count = ci.Count
            });

            return View(items);
        }

        public async Task<IActionResult> Add(int id, int count)
        {
            var product = await ticketsService.GetTicketById(id);
            await ticketsCartService.AddItemToCart(await GetSetCartId(), product, count);
            return RedirectToAction("Index", "Store");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await ticketsService.GetTicketById(id);
            await ticketsCartService.RemoveItemFromCart(await GetSetCartId(), product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCount(int id, int count)
        {
            var product = await ticketsService.GetTicketById(id);
            var cartId = await GetSetCartId();
            if (count > 0)
                await ticketsCartService.ChangeItemCount(cartId, product, count);
            else
                await ticketsCartService.RemoveItemFromCart(cartId, product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            await ticketsCartService.ClearCart(await GetSetCartId());
            return RedirectToAction("Index", "Store");
        }

        private async Task<int> GetSetCartId()
        {
            if (HttpContext.Request.Cookies.ContainsKey(CartCookieName))
                return int.Parse(HttpContext.Request.Cookies[CartCookieName]);

            var id = await ticketsCartService.CreateCart();
            HttpContext.Response.Cookies.Append(CartCookieName, id.ToString());
            return id;
        }
    }
}
