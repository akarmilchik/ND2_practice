using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    public class MyOrdersController : Controller
    {
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly IOrdersService ordersService;

        public MyOrdersController(IOrdersService ordersService, IStringLocalizer<HomeController> localizer)
        {
            this.ordersService = ordersService;
            this.localizer = localizer;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = localizer["My orders"];
            var model = new ShopViewModel
            {
                Orders = (await ordersService.GetOrders()).ToArray()
            };

            return View(model);
        }
    }
}
