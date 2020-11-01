using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    public class TicketsController : Controller
    {
        private readonly IStringLocalizer<TicketsController> localizer;
        private readonly ITicketsService ticketsService;
        private readonly ITicketsCartService ticketsCartService;
        private readonly ITakeDataService takeDataService;
        private readonly ILogger<TicketsController> logger;

        public TicketsController(ITicketsService ticketsService, ITicketsCartService ticketsCartService, ITakeDataService takeDataService, IStringLocalizer<TicketsController> localizer, ILogger<TicketsController> logger)
        {
            this.localizer = localizer;
            this.ticketsService = ticketsService;
            this.ticketsCartService = ticketsCartService;
            this.takeDataService = takeDataService;
            this.logger = logger;
        }
        /*
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = localizer["ticketstitle"];

            var model = new TicketsViewModel
            {
                Tickets = (await takeDataService.GetTickets()).ToArray(),
                Users = (await takeDataService.GetUsers()).ToArray()
            };
            return View(model);
        }*/

        public async Task<IActionResult> GetEventTickets(int eventId)
        {
            ViewData["Title"] = localizer["title"];

            var eventTickets = await ticketsService.GetEventWithTickets(eventId);

            Dictionary<byte, string> statusesDic = new Dictionary<byte, string>();
            foreach (TicketStatuses statuses in Enum.GetValues(typeof(TicketStatuses)))
            {
                statusesDic.Add((byte)statuses, statuses.ToString());
            }

            ViewBag.ticketStatuses = statusesDic;

            return View("EventTickets", eventTickets);
        }

        
        [Authorize]
        public async Task<IActionResult> MyTickets(byte status, string userName)
        {
            ViewData["Title"] = localizer["My tickets"];

            var model = new MyTicketsViewModel
            {
                Tickets = (await ticketsService.GetTickets(status, userName)).ToArray(),
                Events = (await takeDataService.GetEvents()).ToArray(), 
                Users = (await takeDataService.GetUsers()).ToArray()
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


        [Authorize]
        public IActionResult CreateTicket()
        {
            ViewData["Title"] = "Create ticket";
            var users = takeDataService.GetUsers().Result;
            var events = takeDataService.GetEvents().Result;

            var listEvents = new SelectList(events, "Id", "Name");

            ViewBag.Users = users;
            ViewBag.Events = listEvents;

            return View("TicketCreateEdit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CreateTicket(TicketCreateEditModel model)
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
