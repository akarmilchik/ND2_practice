using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsResale.Models.Service;

namespace TicketsResale.Context
{
    public class CartIdHandler : IMiddleware
    {

        private readonly ITicketsCartService ticketsCartService;

        public CartIdHandler(ITicketsCartService ticketsCartService)
        {
            this.ticketsCartService = ticketsCartService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Cookies.ContainsKey(Constants.CartCookieName))
            {
                var id = await ticketsCartService.CreateCart();
                context.Response.Cookies.Append(Constants.CartCookieName, id.ToString());
            }
            else
            {
                if ((context.Request.Cookies.Where(c => c.Key == Constants.CartCookieName).Select(c => c.Value).FirstOrDefault()) == "0")
                {
                    var id = await ticketsCartService.CreateCart();

                    context.Response.Cookies.Append(Constants.CartCookieName, id.ToString());
                }
                else if (context.User.Identity.IsAuthenticated)
                {
                    ClaimsPrincipal currentUser = context.User;

                    var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var cartId = await ticketsCartService.GetTicketsCartIdByUserId(currentUserID);

                    context.Response.Cookies.Delete(Constants.CartCookieName);
                    context.Response.Cookies.Append(Constants.CartCookieName, cartId.ToString());
                    
                }
            }

            await next(context);
        }
    }
}
