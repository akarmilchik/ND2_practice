﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Context;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private const string CartCookieName = "TicketsResale.TicketsCartId";
        private readonly ITicketsCartService ticketsCartService;
        private readonly ITicketsService ticketsService;

        public CartController(ITicketsCartService ticketsCartService, ITicketsService ticketsService)
        {
            this.ticketsCartService = ticketsCartService;
            this.ticketsService = ticketsService;
        }

        public async Task<IActionResult> Index()
        {
            var cartId = HttpContext.GetTicketsCartId();
            var cart = await ticketsCartService.FindCart(cartId);
            var items = cart.CartItems.Select(ci => new TicketsCartViewModel
            {
                TicketId = ci.TicketId,
                TicketName = ci.Ticket.Event.Name,
                Count = ci.Count
            });

            return View(items);
        }

        public async Task<IActionResult> Add(int id, int count)
        {
            var product = await ticketsService.GetTicketById(id);
            await ticketsCartService.AddItemToCart(HttpContext.GetTicketsCartId(), product, count);
            return RedirectToAction("Index", "Store");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await ticketsService.GetTicketById(id);
            await ticketsCartService.RemoveItemFromCart(HttpContext.GetTicketsCartId(), product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCount(int id, int count)
        {
            var product = await ticketsService.GetTicketById(id);
            var cartId = HttpContext.GetTicketsCartId();
            if (count > 0)
                await ticketsCartService.ChangeItemCount(cartId, product, count);
            else
                await ticketsCartService.RemoveItemFromCart(cartId, product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            await ticketsCartService.ClearCart(HttpContext.GetTicketsCartId());
            return RedirectToAction("Index", "Store");
        }

    }
}
