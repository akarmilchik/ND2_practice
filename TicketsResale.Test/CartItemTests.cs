using NUnit.Framework;
using System;
using Moq;
using Microsoft.IdentityModel.Tokens;
using TicketsResale.Business.Models;
using System.Collections.Generic;

namespace TicketsResale.Test
{
    [TestFixture]
    class CartItemTests
    {
        //private Order order;
        //private Ticket ticket;
        //private TicketsCart ticketsCart;


        [OneTimeSetUp]
        public void Setup()
        {

            var Users = new List<StoreUser>()
            {
                new StoreUser { FirstName = "Alexey", LastName = "Karm", Address = "15, Kosmonavtov Av., Grodno, BLR", Localization = "rus", PhoneNumber = "228228", UserName = "alexey.karm@mail.ru", Email = "alexey.karm@mail.ru"}
            };

            var Cities = new List<City>
            {
                new City { Name = "Grodno" },
                new City { Name = "Minsk" },
                new City { Name = "Barselona" },
                new City { Name = "NewYork" },
                new City { Name = "Tokyo" },
                new City { Name = "Dubai" }
            };

            var Venues = new List<Venue>
            {
                new Venue {Id = 1, Name = "Sovetskaya sq.", Address = "Sovetskaya sq.", City = Cities[0] }

            };

            var Events = new List<Event>()
            {
                new Event { Id = 1, Name = "Festival of national cultures 2021", Venue = Venues[0], Date = new DateTime(2021, 06, 05), Banner = "events/fnk-grodno.jpg", Description = "The <strong>Republican Festival of National Cultures</strong> is a holiday of folklore colors of various peoples living in <em>Belarus</em>. Representatives of <strong>36</strong> nationalities participate in the festival, the attendance of the festival in <strong>2018</strong> was <strong>270</strong> thousand people." }
            };


            var Tickets = new List<Ticket>()
            {
                new Ticket { Event = Events[0], Price = 15, SellerId = Users[0].Id, Status = TicketStatuses.selling },
            };

            var cartItems = new List<Order>()
            {
                new Order { Ticket = Tickets[0], TicketId = Tickets[0].Id, Status = OrderStatuses.waiting, Buyer = Users[0], TrackNumber = "SN23423423RR" }
            };


        }
        
            
          

        
    }
}
