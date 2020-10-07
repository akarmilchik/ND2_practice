using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using TicketsResale.Business;
using TicketsResale.Models;

namespace TicketsResale.Controllers
{
    public class MyOrdersController : Controller
    {
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly ShopRepository shopRepository;
        public MyOrdersController(ShopRepository shopRepository, IStringLocalizer<HomeController> localizer)
        {
            this.shopRepository = shopRepository;
            this.localizer = localizer;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewData["Title"] = localizer["My orders"];
            var model = new ShopViewModel
            {
                Orders = shopRepository.GetOrders()
            };

            return View(model);
        }
    }
}
