using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace TicketsResale.Context
{
    public static class HttpContextExtensions
    {
        [Authorize]
        public static int GetTicketsCartId(this HttpContext context)
        {
            return int.Parse(context.Request.Cookies[Constants.CartCookieName]);
        }
    }
}
