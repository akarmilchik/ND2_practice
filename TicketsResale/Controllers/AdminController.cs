using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<StoreUser> _userManager;
        private readonly ICitiesService citiesService;
        private readonly IVenuesService venuesService;
        private readonly IEventsService eventsService;
        private readonly IUsersAndRolesService usersAndRolesService;
        private readonly ILogger<AdminController> logger;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<StoreUser> userManager,ICitiesService citiesService , IVenuesService venuesService, IEventsService eventsService, IUsersAndRolesService usersAndRolesService, ILogger<AdminController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            this.citiesService = citiesService;
            this.venuesService = venuesService;
            this.eventsService = eventsService;
            this.usersAndRolesService = usersAndRolesService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }
        public async Task<IActionResult> Cities()
        {
            ViewData["Title"] = "Cities";

            var cities = await citiesService.GetCities();

            return View("Cities", cities);
        }
        public async Task<IActionResult> Events()
        {

            ViewData["Title"] = "Events";
            var model = new EventsViewModel
            {
                Events = await eventsService.GetEvents(),
                Venues = await venuesService.GetVenues()
            };

            return View("Events", model);

        }
        public async Task<IActionResult> Venues()
        {
            ViewData["Title"] = "Venues";

            var model = new VenuesViewModel
            {
                Venues = await venuesService.GetVenues(),
                Cities = await citiesService.GetCities()
            };

            return View("Venues", model);
        }
        public async Task<IActionResult> Users()
        {
            ViewData["Title"] = "Users";

            var users = await usersAndRolesService.GetUsers();
            var usersRoles = await usersAndRolesService.GetUsersRolesByUsers(users);
            var roles = await usersAndRolesService.GetRolesByUsersRoles(usersRoles);

            var model = new UsersViewModel
            {
                Users = users,
                UsersRoles = usersRoles,
                Roles = roles
            };
            return View("Users", model);
        }

        //============================================

        public IActionResult CreateCity()
        {
            return View("CreateCity");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(City city)
        {
            await citiesService.AddCityToDb(city);

            return RedirectToAction("Cities");
        }

        public async Task<IActionResult> EditCity(int id)
        {
            if (id != null)
            {
                var city = await citiesService.GetCityById(id);

                if (city != null)
                    return View(city);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditCity(City city)
        {
            await citiesService.UpdCityToDb(city);

            return RedirectToAction("Cities");
        }

        public async Task<IActionResult> DeleteCity(int id)
        {
            if (id != null)
            {
                var city = await citiesService.GetCityById(id);

                if (city != null)
                    return View(city);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCity(City city)
        {
            await citiesService.RemoveCityFromDb(city);

            return RedirectToAction("Cities");
        }

        //============================================

        public async Task<IActionResult> CreateEvent()
        {
            var venues = await venuesService.GetVenues();

            var list = new SelectList(venues, "Id", "Name");

            ViewBag.Venues = list;
            ViewBag.DateToday = DateTime.Today.ToShortDateString();

            return View("CreateEvent");
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventCreateViewModel @event)
        {
            if (ModelState.IsValid)
            {
                var fileName = eventsService.SaveFileAndGetName(@event);
                
                Event eventobj = new Event { Name = @event.Name, Venue = @event.Venue, VenueId = @event.VenueId, Date = @event.Date, Banner = "events/" + fileName, Description = @event.Description };
                
                await eventsService.AddEventToDb(eventobj);

            }
            return RedirectToAction("Events");
        }

        public async Task<IActionResult> EditEvent(int? id)
        {
            if (id != null)
            {
                var events = await eventsService.GetEvents();

                Event eevent = events.FirstOrDefault(p => p.Id == id);

                var venues = await venuesService.GetVenues();

                var list = new SelectList(venues, "Id", "Name");

                ViewBag.Venues = list;

                if (eevent != null)
                    return View(eevent);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(Event eevent)
        {
            await eventsService.UpdEventToDb(eevent);

            return RedirectToAction("Events");
        }

        public async Task<IActionResult> DeleteEvent(int? id)
        {
            if (id != null)
            {
                var events = await eventsService.GetEvents();
                var venues = await venuesService.GetVenues();
                
                Event eevent = events.FirstOrDefault(p => p.Id == id);

                if (eevent != null)
                {
                    ViewBag.VenueName = venues.Where(v => v.Id == eevent.VenueId).Select(v=> v.Name).FirstOrDefault();
                    return View(eevent);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(Event eevent)
        {
            await eventsService.RemoveEventFromDb(eevent);

            return RedirectToAction("Events");
        }

        //============================================

        public async Task<IActionResult> CreateVenue()
        {
            var cities = await citiesService.GetCities();

            var list = new SelectList(cities, "Id", "Name");

            ViewBag.Cities = list;

            return View("CreateVenue");
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenue(Venue venue)
        {
            await venuesService.AddVenueToDb(venue);

            return RedirectToAction("Venues");
        }

        public async Task<IActionResult> EditVenue(int? id)
        {
            if (id != null)
            {
                var venues = await venuesService.GetVenues();
                var cities = await citiesService.GetCities();


                var list = new SelectList(cities, "Id", "Name");

                ViewBag.Cities = list;

                Venue venue = venues.FirstOrDefault(p => p.Id == id);

                if (venue != null)
                    return View(venue);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditVenue(Venue venue)
        {
            await venuesService.UpdVenueToDb(venue);

            return RedirectToAction("Venues");
        }

        public async Task<IActionResult> DeleteVenue(int? id)
        {
            if (id != null)
            {
                var venues = await venuesService.GetVenues();
                var cities = await citiesService.GetCities();

                Venue venue = venues.FirstOrDefault(p => p.Id == id);

                if (venue != null)
                {
                    ViewBag.CityName = cities.Where(v => v.Id == venue.CityId).Select(v => v.Name).FirstOrDefault();
                    return View(venue);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVenue(Venue venue)
        {
            await venuesService.RemoveVenueFromDb(venue);

            return RedirectToAction("Venues");
        }

        //========================================================

        public async Task<IActionResult> EditUser(string id)
        {
            if (id != null && id != "")
            {
                var users = await usersAndRolesService.GetUsers();
                var usersRoles = await usersAndRolesService.GetUsersRolesByUsers(users);
                var roles = await usersAndRolesService.GetRolesByUsersRoles(usersRoles);


                var list = new SelectList(roles, "Id", "Name");

                ViewBag.Roles = list;

                StoreUser user = users.FirstOrDefault(p => p.Id == id);

                var model = new UsersRolesViewModel
                {
                    User = user,
                    Role = roles.Where(r => r.Id == usersRoles.Where(ur => ur.UserId == id).Select(ur => ur.RoleId).FirstOrDefault()).FirstOrDefault(),
                    UserRole = usersRoles.Where(ur => ur.UserId == id).Select(ur => ur).FirstOrDefault(),
                    UserId = id,
                    FirstRoleId = usersRoles.Where(ur => ur.UserId == id).Select(ur => ur.RoleId).FirstOrDefault()
                };


                if (model != null)
                    return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UsersRolesViewModel model)
        {

            var users = await usersAndRolesService.GetUsers();
            var usersRoles = await usersAndRolesService.GetUsersRolesByUsers(users);
            var roles = await usersAndRolesService.GetRolesByUsersRoles(usersRoles);

            var user = users.Where(u => u.Id == model.UserId).Select(u => u).FirstOrDefault();
            var oldRole = roles.Where(r => r.Id == usersRoles.Where(ur => ur.UserId == model.UserId).Select(ur => ur).FirstOrDefault().RoleId).Select(r => r.Name).FirstOrDefault();
            var newRole = roles.Where(r => r.Id == model.Role.Id).Select(r => r.Name).FirstOrDefault();
            
            await _userManager.RemoveFromRoleAsync(user, oldRole);
            await _userManager.AddToRoleAsync(user, newRole);
           
            return RedirectToAction("Users");
        }


    }
}

