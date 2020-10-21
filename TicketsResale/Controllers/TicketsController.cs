using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
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

        public TicketsController(ITicketsService ticketsService, IStringLocalizer<TicketsController> localizer)
        { 
            this.localizer = localizer;
            this.ticketsService = ticketsService;
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

            var eventWithTickets = await ticketsService.GetEventWithTickets(eventId);

            return View("EventTickets", eventWithTickets);
        }


        [Authorize]
        public async Task<IActionResult> MyTickets(byte status, string userName)
        {
            ViewData["Title"] = localizer["My tickets"];

            var model = new ShopViewModel
            {
                Tickets = (await ticketsService.GetTickets(status, userName)).ToArray()
            };
            return View(model);
        }

        public async Task<IActionResult> Buy([FromRoute] int id)
        {
            var product = await ticketsService.GetTicketById(id);
            return View("Index", product);
        }


    }
}
