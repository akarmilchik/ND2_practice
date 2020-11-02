using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
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
        private readonly IAddDataService addDataService;
        private readonly ILogger<TicketsController> logger;

        public TicketsController(ITicketsService ticketsService, ITicketsCartService ticketsCartService, ITakeDataService takeDataService, IAddDataService addDataService, IStringLocalizer<TicketsController> localizer, ILogger<TicketsController> logger)
        {
            this.localizer = localizer;
            this.ticketsService = ticketsService;
            this.ticketsCartService = ticketsCartService;
            this.takeDataService = takeDataService;
            this.addDataService = addDataService;
            this.logger = logger;
        }

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
        public async Task<IActionResult> MyTickets(byte ticketStatus, byte orderStatus, string userName)
        {
            ViewData["Title"] = localizer["My tickets"];

            var model = new MyTicketsViewModel
            {
                Tickets = (await ticketsService.GetTickets(ticketStatus, orderStatus, userName)).ToArray(),
                Events = (await takeDataService.GetEvents()).ToArray(),
                Users = (await takeDataService.GetUsers()).ToArray(),
                ticketStatus = ticketStatus
            };
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Buy([FromRoute] int id)
        {
            var ticket = await ticketsService.GetTicketById(id);
            await ticketsCartService.AddItemToCart(HttpContext.GetTicketsCartId(), ticket, 1);
            ticket.Status = 2;
            await addDataService.UpdTicketToDb(ticket);
            return RedirectToAction("Index", "Cart", ticket);
        }


        [Authorize]
        public IActionResult CreateTicket()
        {
            ViewData["Title"] = "Create ticket";

            var events = takeDataService.GetEvents().Result;

            var listEvents = new SelectList(events, "Id", "Name");

            ViewBag.Events = listEvents;

            return View("TicketCreateEdit");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateTicket(TicketCreateEditModel model)
        {
            var user = takeDataService.GetUsers().Result.Where(u => u.UserName == User.Identity.Name).Select(u => u).FirstOrDefault();

            var eevent = takeDataService.GetEvents().Result.Where(e => e.Id == model.Event.Id).Select(u => u).FirstOrDefault();

            if (user != null)
                model.Seller = user;

            if (eevent != null)
                model.Event = eevent;

            Ticket ticket = new Ticket() { EventId = model.Event.Id, Event = model.Event, SellerId = model.Seller.Id, Seller = model.Seller, Price = model.Price, Status = model.Status };

            addDataService.AddTicketToDb(ticket);

            return RedirectToAction("MyTickets", new { ticketStatus = 1, orderStatus = 0, userName = User.Identity.Name });

        }


        [Authorize]
        public IActionResult ConfirmTicket(int? id)
        {
            if (id != null)
            {
                var tickets = takeDataService.GetTickets().Result;
                var events = takeDataService.GetEvents().Result;

                Ticket ticket = tickets.FirstOrDefault(p => p.Id == id);
                

                if (ticket != null)
                {
                    Event eevent = events.FirstOrDefault(p => p.Id == ticket.EventId);
                    var model = new ConfirmRejectTicketViewModel
                    {
                        Ticket = ticket,
                        Event = eevent
                    };

                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public IActionResult ConfirmTicket(ConfirmRejectTicketViewModel model, int ticketId)
        {
            var AllCartItems = takeDataService.GetCartsItems().Result;
            var needCartItems = AllCartItems.Where(ci => ci.TicketId == ticketId).ToList();

            foreach (CartItem cartItem in needCartItems)
            {
                cartItem.Status = model.Confirmation ? (byte)2 : (byte)3;
                addDataService.UpdCartItemToDb(cartItem);
            }

            return RedirectToAction("MyTickets", new { ticketStatus = 1, orderStatus = 0, userName = User.Identity.Name });

        }

    }
}
