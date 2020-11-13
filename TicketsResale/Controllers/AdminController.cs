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

        public async Task<IActionResult> Cities(int page, int pageSize = 5)
        {
            ViewData["Title"] = "Cities";

            var cities = await citiesService.GetCities(page, pageSize);
            var pages = citiesService.GetCitiesPages(pageSize);

            ViewBag.Pages = pages;
            ViewBag.CurrentPage = page;
            ViewBag.NearPages = citiesService.GetNearPages(pages, page);

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
            var city = await citiesService.GetCityById(id);

            if (city != null)
                return View(city);

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
            var city = await citiesService.GetCityById(id);

            if (city != null)
                return View(city);
            
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

        public async Task<IActionResult> EditEvent(int id)
        {
            var @event = await eventsService.GetEventById(id);

            var venues = await venuesService.GetVenues();

            var list = new SelectList(venues, "Id", "Name");

            ViewBag.Venues = list;

            if (@event != null)
                return View(@event);
            
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(Event @event)
        {
            await eventsService.UpdEventToDb(@event);

            return RedirectToAction("Events");
        }

        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await eventsService.GetEventById(id);
            var venueName = venuesService.GetVenueNameById(@event.VenueId);

            if (@event != null)
            {
                ViewBag.VenueName = venueName;
                return View(@event);
            }
           
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(Event @event)
        {
            await eventsService.RemoveEventFromDb(@event);

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

        public async Task<IActionResult> EditVenue(int id)
        {
            var venue = await venuesService.GetVenueById(id);
            var cities = await citiesService.GetCities();

            var list = new SelectList(cities, "Id", "Name");

            ViewBag.Cities = list;

            if (venue != null)
                return View(venue);
            
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditVenue(Venue venue)
        {
            await venuesService.UpdVenueToDb(venue);

            return RedirectToAction("Venues");
        }

        public async Task<IActionResult> DeleteVenue(int id)
        {
            var venue = await venuesService.GetVenueById(id);
            var cityName = await citiesService.GetCityNameById(venue.CityId);

            if (venue != null)
            {
                ViewBag.CityName = cityName;
                return View(venue);
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
                var user = await usersAndRolesService.GetUserById(id);
                var userRole = await usersAndRolesService.GetUserRoleByUser(user);
                var userFirstRole = await usersAndRolesService.GetRoleByUserRole(userRole);
                var roles = await usersAndRolesService.GetRoles();
                var list = new SelectList(roles, "Id", "Name");

                ViewBag.Roles = list;

                var model = new UsersRolesViewModel
                {
                    User = user,
                    Role = userFirstRole,
                    UserRole = userRole,
                    UserId = id,
                    FirstRoleId = userFirstRole.Id
                };


                if (model != null)
                    return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UsersRolesViewModel model)
        {
            var user = await usersAndRolesService.GetUserById(model.UserId);
            var userRole = await usersAndRolesService.GetUserRoleByUser(user);
            var oldRole = await usersAndRolesService.GetRoleByUserRole(userRole);
            var newRole = await usersAndRolesService.GetRoleByUserRoleId(model.Role.Id);

            await _userManager.RemoveFromRoleAsync(user, oldRole.Name);
            await _userManager.AddToRoleAsync(user, newRole.Name);
           
            return RedirectToAction("Users");
        }


    }
}

