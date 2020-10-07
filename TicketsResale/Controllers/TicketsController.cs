using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Net;
using TicketsResale.Business;
using TicketsResale.Business.Models;
using TicketsResale.Models;

namespace TicketsResale.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ShopRepository shopRepository;
        private readonly IStringLocalizer<TicketsController> localizer;

        public TicketsController(ShopRepository shopRepository, IStringLocalizer<TicketsController> localizer)
        {
            this.shopRepository = shopRepository;
            this.localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = localizer["ticketstitle"];

            var model = new ShopViewModel
            {
                Tickets = shopRepository.GetTickets()
            };
            return View(model);
        }

        public IActionResult GetEventTickets(int eventId)
        {
            ViewData["Title"] = localizer["title"];

            var eventWithTickets = shopRepository.GetEventWithTickets(eventId);

            return View("EventTickets", eventWithTickets);
        }


        [Authorize]
        public IActionResult MyTickets(TicketStatuses status, string userName)
        {
            ViewData["Title"] = localizer["My tickets"];

            var model = new ShopViewModel
            {
                Tickets = shopRepository.GetTickets(status, userName)
            };
            return View(model);
        }

        public IActionResult Buy([FromRoute] int id)
        {
            var product = shopRepository.GetTicketById(id);
            return View("Index", product);
        }


    }
}
