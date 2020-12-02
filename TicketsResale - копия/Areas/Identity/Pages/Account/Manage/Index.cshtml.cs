using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using TicketsResale.Business.Models;
using TicketsResale.Models;

namespace TicketsResale.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly IUsersAndRolesService usersAndRolesService;

        public IndexModel(
            UserManager<StoreUser> userManager,
            SignInManager<StoreUser> signInManager,
            IUsersAndRolesService usersAndRolesService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.usersAndRolesService = usersAndRolesService;
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Localization { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Display(Name = "Last name")]
            public string LastName { get; set; }

            [Display(Name = "Address")]
            public string Address { get; set; }

            [Display(Name = "Localization")]
            public Localizations Localization { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

        }

        private async Task LoadAsync(StoreUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var userData = await usersAndRolesService.GetUserDataAsync(user);
            var firstName = userData.FirstName;
            var lastName = userData.LastName;
            var address = userData.Address;
            var localization = userData.Localization;

            Username = userName;

            var enumList = EnumHelper.GetSelectList(typeof(Localizations));

            var list = new SelectList(enumList.AsEnumerable(), "Value", "Text");

            ViewData["Localization"] = list;


            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Localization = localization
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var firstName = await usersAndRolesService.GetUserFirstNameByUserName(user.UserName);
            if (Input.FirstName != firstName)
            {
                await usersAndRolesService.UpdUserFirstName(user, Input.FirstName);
            }

            var lastName = await usersAndRolesService.GetUserLastNameByUserName(user.UserName);
            if (Input.LastName != lastName)
            {
                await usersAndRolesService.UpdUserLastName(user, Input.LastName);
            }

            var address = await usersAndRolesService.GetUserAddressByUserName(user.UserName);
            if (Input.Address != address)
            {
                await usersAndRolesService.UpdUserAddress(user, Input.Address);

            }

            var localization = await usersAndRolesService.GetUserLocalizationByUserName(user.UserName);
            if (Input.Localization != localization)
            {
                await usersAndRolesService.UpdUserLocalization(user, Input.Localization);
                
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
