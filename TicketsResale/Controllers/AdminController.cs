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
        private readonly ITakeDataService takeDataService;
        private readonly IAddDataService addDataService;
        private readonly ILogger<AdminController> logger;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<StoreUser> userManager, ITakeDataService takeDataService, IAddDataService addDataService, ILogger<AdminController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            this.takeDataService = takeDataService;
            this.addDataService = addDataService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }
        public async Task<IActionResult> Cities()
        {
            ViewData["Title"] = "Cities";

            var cities = (await takeDataService.GetCities()).ToArray();

            return View("Cities", cities);
        }
        public async Task<IActionResult> Events()
        {

            ViewData["Title"] = "Events";
            var model = new EventsViewModel
            {
                Events = (await takeDataService.GetEvents()).ToArray(),
                Venues = (await takeDataService.GetVenues()).ToArray()
            };

            return View("Events", model);

        }
        public async Task<IActionResult> Venues()
        {
            ViewData["Title"] = "Venues";

            var model = new EventsViewModel
            {
                Venues = (await takeDataService.GetVenues()).ToArray(),
                Cities = (await takeDataService.GetCities()).ToArray()
            };

            return View("Venues", model);

        }

        public async Task<IActionResult> Users()
        {
            ViewData["Title"] = "Users";
            var model = new UsersViewModel
            {
                Users = (await takeDataService.GetUsers()).ToArray(),
                Roles = (await takeDataService.GetRoles()).ToArray(),
                UsersRoles = (await takeDataService.GetUsersRoles()).ToArray()
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
            await addDataService.AddCityToDb(city);

            return RedirectToAction("Cities");
        }

        public async Task<IActionResult> EditCity(int? id)
        {
            if (id != null)
            {
                var cities = (await takeDataService.GetCities()).ToArray();

                City city = cities.FirstOrDefault(p => p.Id == id);

                if (city != null)
                    return View(city);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditCity(City city)
        {
            await addDataService.UpdCityToDb(city);

            return RedirectToAction("Cities");
        }

        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id != null)
            {
                var cities = (await takeDataService.GetCities()).ToArray();

                City city = cities.FirstOrDefault(p => p.Id == id);

                if (city != null)
                    return View(city);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCity(City city)
        {
            await addDataService.RemoveCityFromDb(city);

            return RedirectToAction("Cities");
        }

        //============================================

        public async Task<IActionResult> CreateEvent()
        {
            var venues = (await takeDataService.GetVenues()).ToArray();

            var list = new SelectList(venues, "Id", "Name");

            ViewBag.Venues = list;
            ViewBag.DateToday = DateTime.Today.ToShortDateString();

            return View("CreateEvent");
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventCreateViewModel eevent)
        {
            if (ModelState.IsValid)
            {
                var fileName = $"{ Path.GetRandomFileName()}" + $".{Path.GetExtension(eevent.Banner.FileName)}";

                using (var stream = System.IO.File.Create(Path.Combine("wwwroot/img/events/", fileName)))
                {
                    await eevent.Banner.CopyToAsync(stream);
                }

                Event resEvent = new Event { Name = eevent.Name, Venue = eevent.Venue, VenueId = eevent.VenueId, Date = eevent.Date, Banner = "events/" + fileName, Description = eevent.Description };
                
                await addDataService.AddEventToDb(resEvent);

            }
            return RedirectToAction("Events");
        }

        public async Task<IActionResult> EditEvent(int? id)
        {
            if (id != null)
            {
                var events = (await takeDataService.GetEvents()).ToArray();

                Event eevent = events.FirstOrDefault(p => p.Id == id);

                var venues = (await takeDataService.GetVenues()).ToArray();

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
            await addDataService.UpdEventToDb(eevent);

            return RedirectToAction("Events");
        }

        public async Task<IActionResult> DeleteEvent(int? id)
        {
            if (id != null)
            {
                var events = (await takeDataService.GetEvents()).ToArray();
                var venues = (await takeDataService.GetVenues()).ToArray();
                
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
            await addDataService.RemoveEventFromDb(eevent);

            return RedirectToAction("Events");
        }

        //============================================

        public async Task<IActionResult> CreateVenue()
        {
            var cities = (await takeDataService.GetCities()).ToArray();

            var list = new SelectList(cities, "Id", "Name");

            ViewBag.Cities = list;

            return View("CreateVenue");
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenue(Venue venue)
        {
            await addDataService.AddVenueToDb(venue);

            return RedirectToAction("Venues");
        }

        public async Task<IActionResult> EditVenue(int? id)
        {
            if (id != null)
            {
                var venues = (await takeDataService.GetVenues()).ToArray();
                var cities = (await takeDataService.GetCities()).ToArray();


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
            await addDataService.UpdVenueToDb(venue);

            return RedirectToAction("Venues");
        }

        public async Task<IActionResult> DeleteVenue(int? id)
        {
            if (id != null)
            {
                var venues = (await takeDataService.GetVenues()).ToArray();
                var cities = (await takeDataService.GetCities()).ToArray();

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
            await addDataService.RemoveVenueFromDb(venue);

            return RedirectToAction("Venues");
        }

        //========================================================

        public async Task<IActionResult> EditUser(string? id)
        {
            if (id != null && id != "")
            {
                var users = (await takeDataService.GetUsers()).ToArray();
                var roles = (await takeDataService.GetRoles()).ToArray();
                var usersRoles = (await takeDataService.GetUsersRoles()).ToArray();


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

            var dbRoles = (await takeDataService.GetRoles()).ToArray();
            var users = (await takeDataService.GetUsers()).ToArray();
            var usersRoles = (await takeDataService.GetUsersRoles()).ToArray();

            var user = users.Where(u => u.Id == model.UserId).Select(u => u).FirstOrDefault();
            var oldRole = dbRoles.Where(r => r.Id == usersRoles.Where(ur => ur.UserId == model.UserId).Select(ur => ur).FirstOrDefault().RoleId).Select(r => r.Name).FirstOrDefault();
            var newRole = dbRoles.Where(r => r.Id == model.Role.Id).Select(r => r.Name).FirstOrDefault();
            
            await _userManager.RemoveFromRoleAsync(user, oldRole);
            await _userManager.AddToRoleAsync(user, newRole);
           
            return RedirectToAction("Users");
        }


    }
}

