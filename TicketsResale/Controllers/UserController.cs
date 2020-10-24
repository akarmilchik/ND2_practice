using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TicketsResale.Business.Models;
using TicketsResale.Models;

namespace TicketsResale.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager userManager;
        private readonly IStringLocalizer<UserController> localizer;

        public UserController(UserManager userManager, IStringLocalizer<UserController> localizer)
        {
            this.userManager = userManager;
            this.localizer = localizer;
        }
        
        public IActionResult Login(string returnUrl)
        {
            ViewData["Title"] = localizer["Login"];
            var headers = Request.GetTypedHeaders();
            if (string.IsNullOrEmpty(returnUrl) && headers.Referer != null)
                returnUrl = HttpUtility.UrlEncode(headers.Referer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl)
        {
            try
            {/*
                var res = await userManager.ValidatePassword(model.UserName, model.Password);

                if (!res)
                {
                    ModelState.AddModelError(nameof(model.Password), localizer["Wrong password"]);
                    return View();
                }*/

                //var role = await userManager.GetRole(model.UserName);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, "role")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");

            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("UserName", ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
