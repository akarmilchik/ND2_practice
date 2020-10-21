using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Business.Models
{
    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;

        private readonly IStringLocalizer<EventsController> localizer;

        public EventsController(EventsService eventsService, IStringLocalizer<EventsController> localizer)
        {
            this.eventsService = eventsService;
            this.localizer = localizer;
        }
        public async Task<IActionResult> Index()  
        {
            ViewData["Title"] = localizer["eventstitle"];

            var model = new ShopViewModel
            {
                Events = (await eventsService.GetEvents()).ToArray()
            };

            return View(model);
        }

    }
}
