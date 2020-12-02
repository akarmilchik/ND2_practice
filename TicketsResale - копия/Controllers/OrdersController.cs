using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ticketsCartService;
        private readonly ITicketsService ticketsService;
        private readonly IEventsService eventsService;
        private readonly IUsersAndRolesService usersAndRolesService;
        private readonly IOrdersService ordersService;
        private readonly IStringLocalizer<OrdersController> localizer;

        public OrdersController(IOrdersService ticketsCartService, ITicketsService ticketsService, IEventsService eventsService, IUsersAndRolesService usersAndRolesService, IOrdersService ordersService, IStringLocalizer<OrdersController> localizer)
        {
            this.ticketsCartService = ticketsCartService;
            this.ticketsService = ticketsService;
            this.eventsService = eventsService;
            this.usersAndRolesService = usersAndRolesService;
            this.ordersService = ordersService;
            this.localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = localizer["ordersTitle"];

            ClaimsPrincipal currentUser = User;

            var tickets = await ticketsService.GetTicketsByUserId(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);
            var events = await eventsService.GetEvents();
            var sellers = await usersAndRolesService.GetUsers();
            var orders = await ordersService.GetOrdersByUserName(currentUser.Identity.Name);

            var model =  new OrdersViewModel
            {
               Events = events,
               Orders = orders,
               Tickets = tickets,
               Sellers = sellers
            };

            return View(model);
        }
    }
}
