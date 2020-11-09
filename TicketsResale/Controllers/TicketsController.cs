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
        private readonly IEventsService eventsService;
        private readonly ITakeDataService takeDataService;
        private readonly IAddDataService addDataService;
        private readonly ILogger<TicketsController> logger;

        public TicketsController(ITicketsService ticketsService, ITicketsCartService ticketsCartService, IEventsService eventsService, ITakeDataService takeDataService, IAddDataService addDataService, IStringLocalizer<TicketsController> localizer, ILogger<TicketsController> logger)
        {
            this.localizer = localizer;
            this.ticketsService = ticketsService;
            this.ticketsCartService = ticketsCartService;
            this.eventsService = eventsService;
            this.takeDataService = takeDataService;
            this.addDataService = addDataService;
            this.logger = logger;
        }

        public async Task<IActionResult> GetEventTickets(int eventId)
        {
            ViewData["Title"] = localizer["title"];

            var eventTickets = await eventsService.GetEventWithTickets(eventId);

            Dictionary<byte, string> statusesDic = new Dictionary<byte, string>();
            foreach (TicketStatuses statuses in Enum.GetValues(typeof(TicketStatuses)))
            {
                statusesDic.Add((byte)statuses, statuses.ToString());
            }

            ViewBag.ticketStatuses = statusesDic;

            return View("EventTickets", eventTickets);
        }


        [Authorize]
        public async Task<IActionResult> MyTickets(TicketStatuses ticketStatus, CartItemStatuses orderStatus, string userName)
        {
            ViewData["Title"] = localizer["My tickets"];

            var model = new MyTicketsViewModel
            {
                Tickets = await ticketsService.GetTicketsByStatusesAndUserName(ticketStatus, orderStatus, userName),
                Events = await takeDataService.GetEvents(),
                Users = await takeDataService.GetUsers(),
                ticketStatus = ticketStatus
            };
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Buy([FromRoute] int id)
        {
            var ticket = await ticketsService.GetTicketById(id);
            await ticketsCartService.AddItemToCart(HttpContext.GetTicketsCartId(), ticket, 1);
            ticket.Status = TicketStatuses.sold;
            await addDataService.UpdTicketToDb(ticket);
            return RedirectToAction("Index", "Cart", ticket);
        }


        [Authorize]
        public async Task<IActionResult> CreateTicket()
        {
            ViewData["Title"] = "Create ticket";

            var events = await takeDataService.GetEvents();

            var listEvents = new SelectList(events, "Id", "Name");

            ViewBag.Events = listEvents;

            return View("TicketCreateEdit");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTicket(TicketCreateEditModel model)
        {
            var user = (await takeDataService.GetUsers()).ToArray().Where(u => u.UserName == User.Identity.Name).Select(u => u).FirstOrDefault();

            var eevent = (await takeDataService.GetEvents()).ToArray().Where(e => e.Id == model.Event.Id).Select(u => u).FirstOrDefault();
            
            var venue = (await takeDataService.GetVenues()).ToArray().Where(e => e.Id == eevent.VenueId).Select(u => u).FirstOrDefault();

            var city = (await takeDataService.GetCities()).ToArray().Where(e => e.Id == venue.CityId).Select(u => u).FirstOrDefault();

            if (venue != null)
                eevent.Venue = venue;

            if (city != null)
                eevent.Venue.City = city;

            if (user != null)
                model.Seller = user;

            if (eevent != null)
                model.Event = eevent;

            Ticket ticket = new Ticket() { EventId = model.Event.Id, Event = model.Event, SellerId = model.Seller.Id, Seller = model.Seller, Price = model.Price, Status = model.Status };

            await addDataService.AddTicketToDb(ticket);

            return RedirectToAction("MyTickets", new { ticketStatus = 1, orderStatus = 0, userName = User.Identity.Name });

        }


        [Authorize]
        public async Task<IActionResult> ConfirmTicket(int? id)
        {
            if (id != null)
            {
                var tickets = (await takeDataService.GetTickets()).ToArray();
                var events = (await takeDataService.GetEvents()).ToArray();

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
        public async Task<IActionResult> ConfirmTicket(ConfirmRejectTicketViewModel model, int ticketId)
        {
            var AllCartItems = await takeDataService.GetCartsItems();

            var needCartItems = AllCartItems.Where(ci => ci.TicketId == ticketId).ToList();

            var needTicket = (await takeDataService.GetTickets()).Where(ci => ci.Id == ticketId).FirstOrDefault();

            foreach (CartItem cartItem in needCartItems)
            {
                cartItem.Status = model.Confirmation ? CartItemStatuses.confirmed : CartItemStatuses.rejected;

                await addDataService.UpdCartItemToDb(cartItem);
            }

            if (model.Confirmation)
            {
                needTicket.Status = TicketStatuses.sold;
                await addDataService.UpdTicketToDb(needTicket);
            }

            return RedirectToAction("MyTickets", new { ticketStatus = 1, orderStatus = 0, userName = User.Identity.Name });

        }

    }
}
