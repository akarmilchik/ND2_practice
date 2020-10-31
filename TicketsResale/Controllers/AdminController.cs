using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
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

            var cities = await takeDataService.GetCities();

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
            return View("Cities", model);
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
                var cities = await takeDataService.GetCities();

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
                var cities = await takeDataService.GetCities();

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
        
        public IActionResult CreateEvent()
        {
            var venues = takeDataService.GetVenues().Result;
            var list = new SelectList(venues, "Id", "Name");
            ViewBag.Venues = list;
            
            return View("CreateEvent");
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event eevent)
        {
            await addDataService.AddEventToDb(eevent);

            ViewBag.DateToday = DateTime.Today.ToShortDateString();

            return RedirectToAction("Events");
        }

        public async Task<IActionResult> EditEvent(int? id)
        {
            if (id != null)
            {
                var events = await takeDataService.GetEvents();

                Event eevent = events.FirstOrDefault(p => p.Id == id);

                var venues = takeDataService.GetVenues().Result;

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
                var events = await takeDataService.GetEvents();
                var venues = takeDataService.GetVenues().Result;
                
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

        public IActionResult CreateVenue()
        {
            var cities = takeDataService.GetCities().Result;
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
                var venues = await takeDataService.GetVenues();
                var cities = await takeDataService.GetCities();


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
                var venues = await takeDataService.GetVenues();
                var cities = takeDataService.GetCities().Result;

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
            if (id != null)
            {
                var users = await takeDataService.GetUsers();
                var roles = await takeDataService.GetRoles();
                var usersRoles = await takeDataService.GetUsersRoles();
                

                var list = new SelectList(roles, "Id", "Name");

                ViewBag.Roles = list;
                ViewBag.UsersRoles = usersRoles;

                StoreUser user = users.FirstOrDefault(p => p.Id == id);

                if (user != null)
                    return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(StoreUser user)
        {
            await addDataService.UpdUserToDb(user);

            return RedirectToAction("Venues");
        }

       

        /*
         * [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }
        








        
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        


        public async Task<IActionResult> EditRole(string userId)
        {
            StoreUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var allRoles = _roleManager.Roles.ToList();

                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(string userId, List<string> roles)
        {
            StoreUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var allRoles = _roleManager.Roles.ToList();

                var addedRoles = roles.Except(userRoles);

                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }*/

    }
}

