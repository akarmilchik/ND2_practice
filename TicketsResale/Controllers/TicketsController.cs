using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TicketsResale.Business;
using TicketsResale.Business.Models;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IStringLocalizer<TicketsController> localizer;
        private readonly ITicketsService ticketsService;
        private readonly IEventsService eventsService;
        private readonly ILogger<TicketsController> logger;

        public TicketsController(ITicketsService ticketsService, IEventsService eventsService, IStringLocalizer<TicketsController> localizer, ILogger<TicketsController> logger)
        {
            this.localizer = localizer;
            this.ticketsService = ticketsService;
            this.eventsService = eventsService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = localizer["ticketstitle"];

            var model = new ShopViewModel
            {
                Tickets = (await ticketsService.GetTickets()).ToArray()
            };
            return View(model);
        }

        public async Task<IActionResult> GetEventTickets(int eventId)
        {
            ViewData["Title"] = localizer["title"];

            var eventTickets = await ticketsService.GetEventWithTickets(eventId);

            return View(eventTickets);
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

        public async Task<IActionResult> Buy([FromRoute] int id)
        {
            var product = await ticketsService.GetTicketById(id);
            return View("Index", product);
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
