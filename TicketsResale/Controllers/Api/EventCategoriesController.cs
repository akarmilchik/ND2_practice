using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsResale.Business.Models;
using TicketsResale.Context;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[FormatFilter]
    public class EventCategoriesController : ControllerBase
    {
        private readonly StoreContext context;
        //private readonly EventsService eventsService;

        public EventCategoriesController(StoreContext context/*, EventsService eventsService*/)
        {
            this.context = context;
            //this.eventsService = eventsService;
        }

        /// <summary>
        ///     Get all events categories
        /// </summary>
        /// <returns>List of EventCategory</returns>
        /// 
        [HttpGet]
        //[ServiceFilter(typeof(CacheFilterAttribute))]
       // [ProducesResponseType(typeof(ICollection<EventCategory>), StatusCodes.Status200OK)]
        [Produces("application/json", "application/xml", "text/csv")]
        public async Task<IEnumerable<EventCategory>> GetCategories()
        { 
            return await context.EventCategories.ToListAsync();
        }

        /// <summary>
        /// Get events category by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>EventCategory object</returns>
        // GET: api/EventCategories/5
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", "text/csv")]
        [ProducesResponseType(typeof(EventCategory), StatusCodes.Status200OK)]
        public async Task<ActionResult<EventCategory>> GetCategory(int id)
        {
            var eventCategory = await context.EventCategories.FindAsync(id);

            if (eventCategory == null) return NotFound();

            return eventCategory;
        }

        /// <summary>
        /// Put event category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eventCategory"></param>
        /// <returns>No content</returns>
        // PUT: api/EventCategories/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCategory(int id, EventCategory eventCategory)
        {
            if (id != eventCategory.Id) return BadRequest();

            context.Entry(eventCategory).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventCategoryExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Post event category
        /// </summary>
        /// <param name="eventCategory"></param>
        /// <returns>Created at action result</returns>
        // POST: api/EventCategories
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventCategory>> PostCategory(EventCategory eventCategory)
        {
            context.EventCategories.Add(eventCategory);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = eventCategory.Id }, eventCategory);
        }

        /// <summary>
        /// Delete event category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted EventCategory object</returns>
        // DELETE: api/EventCategories/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventCategory>> DeleteCategory(int id)
        {
            var eventCategory = await context.EventCategories.FindAsync(id);
            if (eventCategory == null) return NotFound();

            context.EventCategories.Remove(eventCategory);
            await context.SaveChangesAsync();

            return eventCategory;
        }

        private bool EventCategoryExists(int id)
        {
            return context.EventCategories.Any(e => e.Id == id);
        }
    }
}
