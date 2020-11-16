using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace TicketsResale.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult SetLanguage(string locale, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(locale)));

            var headers = Request.GetTypedHeaders();
            if (string.IsNullOrEmpty(returnUrl) && headers.Referer != null)
            {
                returnUrl = headers.Referer.PathAndQuery + "?locale=" + locale;
            }
            else
            {
                returnUrl = returnUrl + "?locale=" + locale;
            }

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }

            return Redirect(returnUrl);

        }
    }
}
