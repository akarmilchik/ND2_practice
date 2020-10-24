using Microsoft.AspNetCore.Http;

namespace TicketsResale.Context
{
    public static class HttpContextExtensions
    {
        public static int GetTicketsCartId(this HttpContext context)
        {
            return int.Parse(context.Request.Cookies[Constants.CartCookieName]);
        }
    }
}
