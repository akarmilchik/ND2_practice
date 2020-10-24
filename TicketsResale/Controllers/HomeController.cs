using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using TicketsResale.Business;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> localizer;
        //private readonly IUsersService usersService;


        public HomeController(IStringLocalizer<HomeController> localizer/*, IUsersService usersService*/)
        {
            //this.usersService = usersService;
            this.localizer = localizer;

        }

        public IActionResult Index()
        {
            ViewData["Title"] = localizer["homepagetitle"];

            return View();
        }
        /*
        [Authorize]
        public async Task<IActionResult> UserInfo(string userName)
        {
            ViewData["Title"] = localizer["UserInfo"];

            var model = new ShopViewModel
            {
                User = await usersService.GetUserByName(userName)
            };

            return View(model);
        }*/

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
