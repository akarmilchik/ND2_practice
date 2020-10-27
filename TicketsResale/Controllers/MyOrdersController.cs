using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Context;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    [Authorize]
    public class MyOrdersController : Controller
    {
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly ITicketsService ticketsService;
        private readonly IEventsService eventsService;
        private readonly ITicketsCartService ticketsCartService;

        public MyOrdersController( ITicketsService ticketsService, IEventsService eventsService, ITicketsCartService ticketsCartService, IStringLocalizer<HomeController> localizer)
        {
            this.ticketsService = ticketsService;
            this.eventsService = eventsService;
            this.ticketsCartService = ticketsCartService;
            this.localizer = localizer;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = localizer["My orders"];
            //var model = new ShopViewModel
            //{
            //    CartItems = (await eventsService.GetCartItems()).ToArray(),
            //    Tickets = (await ticketsService.GetTickets()).ToArray(),
            //    Events = (await eventsService.GetEvents()).ToArray()
            //};


            var cartId = HttpContext.GetTicketsCartId();
            var cart = await ticketsCartService.FindCart(cartId);
            var items = cart.CartItems.Select(ci => new TicketsCartViewModel
            {
                TicketId = ci.TicketId,
                TicketName = ci.Ticket.Event.Name,
                Count = ci.Count
            });

            return View(items);
        }
    }
}
