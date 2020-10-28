using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Context;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    [Authorize]
    public class MyOrdersController : Controller
    {
        private readonly IStringLocalizer<HomeController> localizer;
        private readonly ITicketsService ticketsService;
        private readonly IEventsService eventsService;
        private readonly ITicketsCartService ticketsCartService;

        public MyOrdersController( ITicketsService ticketsService, IEventsService eventsService, ITicketsCartService ticketsCartService, IStringLocalizer<HomeController> localizer)
        {
            this.ticketsService = ticketsService;
            this.eventsService = eventsService;
            this.ticketsCartService = ticketsCartService;
            this.localizer = localizer;
        }


    }
}
