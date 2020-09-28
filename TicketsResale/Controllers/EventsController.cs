using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Localization;
using TicketsResale.Business;
using TicketsResale.Models;

namespace TicketsResale.Business.Models
{
    public class EventsController : Controller
    {
        private readonly ShopRepository shopRepository;
        private readonly IStringLocalizer<EventsController> localizer;

        public EventsController(ShopRepository shopRepository, IStringLocalizer<EventsController> localizer)
        {
            this.shopRepository = shopRepository;
            this.localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = localizer["eventstitle"];

            var model = new ShopViewModel
            { 
                Events = shopRepository.GetEvents() 
            };

            return View(model);
        }

    }
}
