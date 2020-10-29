using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TicketsResale.Business.Models;
using TicketsResale.Context;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    [Authorize(Roles ="Administrator")]
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

            var events = await takeDataService.GetEvents();

            return View("Events", events);
            
        }
        public async Task<IActionResult> Venues()
        {

            ViewData["Title"] = "Venues";

            var venues = await takeDataService.GetVenues();
            var model = new EventsViewModel
            {
                Venues = (await takeDataService.GetVenues()).ToArray(),
                Cities = (await takeDataService.GetCities()).ToArray()
            };

            return View("Venues", model);

        }
        public async Task<IActionResult> Roles()
        {
            ViewData["Title"] = "Roles";

            var roles = await takeDataService.GetUsersRoles();

            return View("Roles", roles);
        }
        

        public IActionResult CreateCity()
        {
            return View("CreateCity");
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCity(City model)
        {

            if (ModelState.IsValid)
            {

                addDataService.AddCityToDb(model);
                logger.LogDebug(JsonConvert.SerializeObject(model));
            }
            else
            {
                return View("CreateCity", model);
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditCity(string name)
        {
            var cities = takeDataService.GetCities();


            var model = new CityCreateEditModel
            {
                Name = name,
                City = cities.Result.Where(s => s.Name == name).Select(s => s).FirstOrDefault()
            };

            return View("EditCity", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCity(City model)
        {

            if (ModelState.IsValid)
            {
                

                addDataService.AddCityToDb(model);
                logger.LogDebug(JsonConvert.SerializeObject(model));
            }
            else
            {
                return View("EditCity", model);
            }
            return RedirectToAction("Index");
        }





        /*
        public async Task<IActionResult> CreateEvent()
        {
            return View();
        }

        public async Task<IActionResult> CreateVenue()
        {
            return View();
        }
        */

        [HttpPost]
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
        /*
        public async Task<IActionResult> DeleteCity()
        {
            return View();
        }
        public async Task<IActionResult> DeleteEvent()
        {
            return View();
        }

        public async Task<IActionResult> DeleteVenue()
        {
            return View();
        }
        */
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
        /*
        public async Task<IActionResult> EditCity()
        {
            return View();
        }

        public async Task<IActionResult> EditEvent()
        {
            return View();
        }

        public async Task<IActionResult> EditVenue()
        {
            return View();
        }*/

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
        }

    }
}
