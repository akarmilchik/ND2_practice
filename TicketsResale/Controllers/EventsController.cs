using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Business.Models
{
    public class EventsController : Controller
    {
        private readonly IStringLocalizer<EventsController> localizer;
        private readonly IEventsService eventsService;
        private readonly IVenuesService venuesService;
        private readonly ICitiesService citiesService;

        public EventsController(IStringLocalizer<EventsController> localizer, IEventsService eventsService, IVenuesService venuesService, ICitiesService citiesService)
        {

            this.localizer = localizer;
            this.eventsService = eventsService;
            this.venuesService = venuesService;
            this.citiesService = citiesService;
        }
        public async Task<IActionResult> Index(int categoryId)  
        {

            ViewData["Title"] = localizer["eventstitle"];
            /*
             var categories = await eventsService.GetEventsCategories();
             var list = new SelectList(categories, "Id", "Name");
             ViewBag.EventsCategories = list;

             //Events = await eventsService.GetEventsByCategoryId(categoryId),

             var model = new EventsViewModel
             {
                 EventsCategories = categories,
                 Events = await eventsService.GetEvents(),
                 Venues = await venuesService.GetVenues(),
                 Cities = await citiesService.GetCities(),
             };
            */

            var categories = await eventsService.GetEventsCategories();
            return View(categories);
        }


        public async Task<IActionResult> Index2()
        {
            ViewData["Title"] = localizer["eventstitle"];

            var categories = await eventsService.GetEventsCategories();

            return View(categories);
        }

    }
}
