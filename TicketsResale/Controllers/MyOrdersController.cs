using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MyOrdersController : Controller
    {
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly ITicketsService ticketsService;
        private readonly IEventsService eventsService;

        public MyOrdersController( ITicketsService ticketsService, IEventsService eventsService, IStringLocalizer<HomeController> localizer)
        {
            this.ticketsService = ticketsService;
            this.eventsService = eventsService;
            this.localizer = localizer;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = localizer["My orders"];
            var model = new ShopViewModel
            {
                CartItems = (await eventsService.GetCartItems()).ToArray(),
                Tickets = (await ticketsService.GetTickets()).ToArray(),
                Events = (await eventsService.GetEvents()).ToArray()
            };

            return View(model);
        }
    }
}
