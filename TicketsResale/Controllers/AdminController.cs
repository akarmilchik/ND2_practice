using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketsResale.Business.Models;
using TicketsResale.Models;

namespace TicketsResale.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AdminController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<StoreUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<StoreUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cities()
        {
            return View();
        }
        public IActionResult CreateCity()
        {
            return View();
        }
        public IActionResult CreateEvent()
        {
            return View();
        }

        public IActionResult CreateVenue()
        {
            return View();
        }


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

        public IActionResult DeleteCity()
        {
            return View();
        }
        public IActionResult DeleteEvent()
        {
            return View();
        }

        public IActionResult DeleteVenue()
        {
            return View();
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

        public IActionResult EditCity()
        {
            return View();
        }

        public IActionResult EditEvent()
        {
            return View();
        }

        public IActionResult EditVenue()
        {
            return View();
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
        }

    }
}
