using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TicketsResale.Business;
using TicketsResale.Models;

namespace TicketsResale.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopRepository shopRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly UserManager userManager;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer, ShopRepository shopRepository, UserManager userManager)
        {
            this.shopRepository = shopRepository;
            _logger = logger;
            this.localizer = localizer;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = localizer["homepagetitle"];



            return View();
        }

        [Authorize]
        public IActionResult UserInfo(string userName)
        {
            ViewData["Title"] = localizer["User info"];

            var userObj = shopRepository.GetUserByName(userName);

            var model = new ShopViewModel
            {
                User = userObj
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = localizer["Privacy"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
