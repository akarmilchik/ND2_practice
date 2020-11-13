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
        private readonly IEventsService eventsService;
        private readonly IVenuesService venuesService;
        private readonly ICitiesService citiesService;
        private readonly IUsersAndRolesService usersAndRolesService;
        private readonly IOrdersService ordersService;

        public TicketsController(ITicketsService ticketsService, IOrdersService ticketsCartService, IEventsService eventsService, IVenuesService venuesService, ICitiesService citiesService, IUsersAndRolesService usersAndRolesService, IOrdersService ordersService, IStringLocalizer<TicketsController> localizer)
        {
            this.localizer = localizer;
            this.ticketsService = ticketsService;
            this.eventsService = eventsService;
            this.venuesService = venuesService;
            this.citiesService = citiesService;
            this.usersAndRolesService = usersAndRolesService;
            this.ordersService = ordersService;
        }

        public async Task<IActionResult> GetEventTickets(int eventId)
        {
            ViewData["Title"] = localizer["title"];

            var eventTickets = await eventsService.GetEventWithTickets(eventId);

            return View("EventTickets", eventTickets);
        }


        [Authorize]
        public async Task<IActionResult> MyTickets(TicketStatuses ticketStatus, string userName)
        {
            ViewData["Title"] = localizer["My tickets"];

            var tickets = await ticketsService.GetTicketsByStatusAndUserName(ticketStatus, userName);

            var model = new MyTicketsViewModel
            {
                Tickets = tickets,
                Events = await eventsService.GetEventsByTickets(tickets),
                Users = await usersAndRolesService.GetUsers(),
                ticketStatus = ticketStatus
            };
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Buy([FromRoute] int id)
        {
            var ticket = await ticketsService.GetTicketById(id);

            await ordersService.AddTicketToOrder(User.Identity.Name, ticket);

            ticket.Status = TicketStatuses.waiting;

            await ticketsService.UpdTicketToDb(ticket);

            return RedirectToAction("Index", "Orders", ticket);
        }


        [Authorize]
        public async Task<IActionResult> CreateTicket()
        {
            ViewData["Title"] = "Create ticket";

            var events = await eventsService.GetEvents();

            var listEvents = new SelectList(events, "Id", "Name");

            ViewBag.Events = listEvents;

            return View("TicketCreateEdit");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTicket(TicketCreateEditModel model)
        {
            var user = await usersAndRolesService.GetUserByUserName(User.Identity.Name);

            var @event = await eventsService.GetEventById(model.Event.Id);

            var venue = await venuesService.GetVenueById(@event.VenueId);

            var city = await citiesService.GetCityById(venue.CityId);

            if (venue != null)
                @event.Venue = venue;

            if (city != null)
                @event.Venue.City = city;

            if (user != null)
                model.Seller = user;

            if (@event != null)
                model.Event = @event;

            Ticket ticket = new Ticket() { EventId = model.Event.Id, Event = model.Event, SellerId = model.Seller.Id, Seller = model.Seller, Price = model.Price, Status = model.Status };

            await ticketsService.AddTicketToDb(ticket);

            return RedirectToAction("MyTickets", new { ticketStatus = 1, orderStatus = 0, userName = User.Identity.Name });

        }


        [Authorize]
        public async Task<IActionResult> ConfirmTicket(int id)
        {
            var ticket = await ticketsService.GetTicketById(id);
            var @event = await eventsService.GetEventById(ticket.EventId);

            if (ticket != null)
            {
                var model = new ConfirmRejectTicketViewModel
                {
                    Ticket = ticket,
                    Event = @event
                };

                return View(model);
            }
           
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ConfirmTicket(ConfirmRejectTicketViewModel model, int ticketId)
        {
            var allOrders = await ordersService.GetOrders();

            var needOrders = allOrders.Where(ci => ci.TicketId == ticketId).ToList();

            var needTicket = await ticketsService.GetTicketById(ticketId);

            foreach (Order order in needOrders)
            {
                order.Status = model.Confirmation ? OrderStatuses.confirmed : OrderStatuses.rejected;

                await ordersService.UpdOrderToDb(order);
            }

            if (model.Confirmation)
            {
                needTicket.Status = TicketStatuses.sold;
                await ticketsService.UpdTicketToDb(needTicket);
            }

            return RedirectToAction("MyTickets", new { ticketStatus = 1, orderStatus = 0, userName = User.Identity.Name });

        }

    }
}
