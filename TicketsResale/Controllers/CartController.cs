using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ITicketsCartService ticketsCartService;
        private readonly ITicketsService ticketsService;
        private readonly ITakeDataService takeDataService;

        public CartController(ITicketsCartService ticketsCartService, ITicketsService ticketsService, ITakeDataService takeDataService)
        {
            this.ticketsCartService = ticketsCartService;
            this.ticketsService = ticketsService;
            this.takeDataService = takeDataService;
        }

        public async Task<IActionResult> Index()
        {
            var cartId = HttpContext.GetTicketsCartId();         
            var cart = await ticketsCartService.FindCart(cartId);
            var tickets = await ticketsService.GetTicketsByCart(cart.Id);
            var events = await takeDataService.GetEvents();
            var sellers = await takeDataService.GetUsers();

            Dictionary<byte, string> statusesDic = new Dictionary<byte, string>();
            foreach (CartItemStatuses statuses in Enum.GetValues(typeof(CartItemStatuses)))
            {
                statusesDic.Add((byte)statuses, statuses.ToString());
            }

            ViewBag.ticketStatuses = statusesDic;


            var model =  new TicketsCartViewModel
            {
               Events = events.ToArray(),
               CartItems = cart.CartItems,
               Tickets = tickets.ToArray(),
               Sellers = sellers.ToArray()
            };

            return View(model);
        }
        /*
        public async Task<IActionResult> Add(int id, int count)
        {
            var product = await ticketsService.GetTicketById(id);
            await ticketsCartService.AddItemToCart(HttpContext.GetTicketsCartId(), product, count);
            return RedirectToAction("Index", "Tickets");
        }*/

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await ticketsService.GetTicketById(id);
            await ticketsCartService.RemoveItemFromCart(HttpContext.GetTicketsCartId(), product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCount(int id, int count)
        {
            var product = await ticketsService.GetTicketById(id);
            var cartId = HttpContext.GetTicketsCartId();
            if (count > 0)
                await ticketsCartService.ChangeItemCount(cartId, product, count);
            else
                await ticketsCartService.RemoveItemFromCart(cartId, product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            await ticketsCartService.ClearCart(HttpContext.GetTicketsCartId());
            return RedirectToAction("Index", "Store");
        }

    }
}
