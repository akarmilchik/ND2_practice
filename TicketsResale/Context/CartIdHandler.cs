using Microsoft.AspNetCore.Http;
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

            await next(context);
        }
    }
}
