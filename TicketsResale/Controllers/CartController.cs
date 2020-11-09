using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
               Events = events,
               CartItems = cart.CartItems,
               Tickets = tickets,
               Sellers = sellers
            };

            return View(model);
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


    }
}
