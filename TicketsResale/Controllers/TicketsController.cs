using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Context;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IStringLocalizer<TicketsController> localizer;
        private readonly ITicketsService ticketsService;
        private readonly IEventsService eventsService;
        private readonly ITicketsCartService ticketsCartService;
        private readonly ILogger<TicketsController> logger;

        public TicketsController(ITicketsService ticketsService, IEventsService eventsService, ITicketsCartService ticketsCartService, IStringLocalizer<TicketsController> localizer, ILogger<TicketsController> logger)
        {
            this.localizer = localizer;
            this.ticketsService = ticketsService;
            this.eventsService = eventsService;
            this.ticketsCartService = ticketsCartService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = localizer["ticketstitle"];

            var model = new ShopViewModel
            {
                Tickets = (await ticketsService.GetTickets()).ToArray(),
                Users = (await eventsService.GetUsers()).ToArray()
            };
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> GetEventTickets(int eventId)
        {
            ViewData["Title"] = localizer["title"];

            var eventTickets = await ticketsService.GetEventWithTickets(eventId);

            return View("EventTickets", eventTickets);
        }


        [Authorize]
        public async Task<IActionResult> MyTickets(byte status, string userName)
        {
            ViewData["Title"] = localizer["My tickets"];

            var model = new ShopViewModel
            {
                Tickets = (await ticketsService.GetTickets(status, userName)).ToArray(),
                Events = (await eventsService.GetEvents()).ToArray()
            };
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Buy([FromRoute] int id)
        {
            var ticket = await ticketsService.GetTicketById(id);
            await ticketsCartService.AddItemToCart(HttpContext.GetTicketsCartId(), ticket, 1);
            return RedirectToAction("Index", "Cart", ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(TicketCreateEditModel model)
        {
            if (ModelState.IsValid)
            {
                logger.LogDebug(JsonConvert.SerializeObject(model));
            }
            else
            {
                return View("TicketCreateEdit", model);
            }
            return RedirectToAction("Index");
        }

    }
}
