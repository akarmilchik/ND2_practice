using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Controllers.Api.Models;
using TicketsResale.Models.Service;
using TicketsResale.Queries;

namespace TicketsResale.Controllers.Api
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly IVenuesService venuesService;
        private readonly ICitiesService citiesService;
        private readonly IMapper mapper;

        public EventsController(IEventsService eventsService, IVenuesService venuesService, ICitiesService citiesService, IMapper mapper)
        {
            this.eventsService = eventsService;
            this.venuesService = venuesService;
            this.citiesService = citiesService;
            this.mapper = mapper;
        }

        //GET
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventsByEventCategoryId([FromQuery] EventQuery query)
        {
            var pagedResult = await eventsService.GetEventsQuery(query);

            HttpContext.Response.Headers.Add("x-total-count", pagedResult.TotalCount.ToString());

            return Ok(mapper.Map<IEnumerable<EventResource>>(pagedResult.Items));
        }
    }
}
