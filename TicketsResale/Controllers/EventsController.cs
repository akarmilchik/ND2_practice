using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Business.Models
{
    public class EventsController : Controller
    {
        private readonly IStringLocalizer<EventsController> localizer;
        private readonly ITakeDataService takeDataService;

        public EventsController(IStringLocalizer<EventsController> localizer, ITakeDataService takeDataService)
        {

            this.localizer = localizer;
            this.takeDataService = takeDataService;
        }
        public async Task<IActionResult> Index()  
        {
            ViewData["Title"] = localizer["eventstitle"];

            var model = new EventsViewModel
            {
                Events = (await takeDataService.GetEvents()).ToArray(),
                Venues = (await takeDataService.GetVenues()).ToArray(),
                Cities = (await takeDataService.GetCities()).ToArray(),
            };

            return View(model);
        }

    }
}
