using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsResale.Business.Models;
using TicketsResale.Context;
using TicketsResale.Controllers.Api.Models;
using TicketsResale.Models.Service;
using TicketsResale.Queries;

namespace TicketsResale.Controllers.Api
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VenuesController : Controller
    {

        private readonly IVenuesService venuesService;
        private readonly IMapper mapper;

        public VenuesController(IVenuesService venuesService, IMapper mapper)
        {
            this.venuesService = venuesService;
            this.mapper = mapper;
        }

        // GET
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVenuesByQuery([FromQuery] VenueQuery query)
        {
            var pagedResult = await venuesService.GetVenuesQuery(query);

            var result = mapper.Map<IEnumerable<VenueResource>>(pagedResult);

            return Ok(result);
        }
    }
}
