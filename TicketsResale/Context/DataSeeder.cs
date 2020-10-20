using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Context
{
    public class DataSeeder
    {
        private readonly StoreContext context;
        public DataSeeder(StoreContext context)
        {
            this.context = context;
        }

        private static readonly List<City> Cities = new List<City>
        {
            new City { Id = 1, Name = "Grodno" },
            new City { Id = 2, Name = "Minsk" },
            new City { Id = 3, Name = "Barselona" },
            new City { Id = 4, Name = "NewYork" },
            new City { Id = 5, Name = "Tokyo" },
            new City { Id = 6, Name = "Dubai" }
        };

        private static readonly List<User> Users = new List<User>
        {
            new User { Id = 1, FirstName = "Alexey", LastName = "Robinson", Address = "adrUser0", Localization = "rus", PhoneNumber = "228228", UserName = "admin", Password = "admin", Role = "Administrator" },
            new User { Id = 2, FirstName = "Jominez", LastName = "Maxwell", Address = "adrUser1", Localization = "spa", PhoneNumber = "345124", UserName = "user", Password = "user", Role = "User" },
            new User { Id = 3, FirstName = "Alibaba", LastName = "Bestseller", Address = "adrUser2", Localization = "eng", PhoneNumber = "777777", UserName = "seller", Password = "seller", Role = "User" }
        };


        private static readonly List<Venue> Venues = new List<Venue>
        {
            new Venue { Id = 1, Name = "Sovetskaya sq.", Address = "Sovetskaya sq.", City = Cities[0] },
            new Venue { Id = 2, Name = "Pyshki forest park", Address = "adrVen1", City = Cities[0] },
            new Venue { Id = 3, Name = "Independense sq.", Address = "adrVen2", City = Cities[1] },
            new Venue { Id = 4, Name = "Central botanical garden", Address = "adrVen3", City = Cities[1] },
            new Venue { Id = 5, Name = "Park Forum", Address = "adrVen4", City = Cities[2] },
            new Venue { Id = 6, Name = "Igulada", Address = "adrVen5", City = Cities[2] },
            new Venue { Id = 7, Name = "Broadway Majestic Theatre", Address = "adrVen6", City = Cities[3] },
            new Venue { Id = 8, Name = "Radio city Rockettes", Address = "adrVen7", City = Cities[3] },
            new Venue { Id = 9, Name = "Fukagawa Sakura", Address = "adrVen8", City = Cities[4] },
            new Venue { Id = 10, Name = "Sanja Matsuri", Address = "adrVen9", City = Cities[4] },
            new Venue { Id = 11, Name = "Dubai world trade center", Address = "adrVen10", City = Cities[5] },
            new Venue { Id = 12, Name = "Global village", Address = "Global village", City = Cities[5] }
        };

        private static readonly List<Event> Events = new List<Event>
        {
            new Event { Id = 1, Name = "Festival of national cultures 2021", Venue = Venues[0], Date = new DateTime(2021, 06, 05), Banner = "events/fnk-grodno.jpg", Description = "desEv0" },
            new Event { Id = 2, Name = "Color Fest 2020", Venue = Venues[1], Date = new DateTime(2020, 09, 22), Banner = "events/colorfest-grodno.jpg", Description = "desEv1" },
            new Event { Id = 3, Name = "Primavera Sound Barselona", Venue = Venues[4], Date = new DateTime(2021, 06, 14), Banner = "events/primavera-barsa.jpg", Description = "desEv2" },
            new Event { Id = 4, Name = "Magic garden. Festival of Music and Theater", Venue = Venues[3], Date = new DateTime(2020, 09, 14), Banner = "events/botanic-minsk.jpg", Description = "desEv3" },
            new Event { Id = 5, Name = "BALLOON FESTIVAL", Venue = Venues[5], Date = new DateTime(2021, 07, 11), Banner = "events/baloon-igulada.jpg", Description = "desEv4" },
            new Event { Id = 6, Name = "The Phantom of the Opera", Venue = Venues[6], Date = new DateTime(2020, 09, 25), Banner = "events/majestic-newyork.jpg", Description = "desEv5" },
            new Event { Id = 7, Name = "ROCKETTES CHRISTMAS SPECTACULAR 2021", Venue = Venues[7], Date = new DateTime(2020, 12, 24), Banner = "events/rockettes-newyork.jpg", Description = "desEv6" },
            new Event { Id = 8, Name = "Edo Fukagawa Sakura Festival", Venue = Venues[8], Date = new DateTime(2021, 04, 05), Banner = "events/fukagawa-sakura.png", Description = "desEv7" },
            new Event { Id = 9, Name = "Sanja Matsuri Festival", Venue = Venues[9], Date = new DateTime(2020, 10, 17), Banner = "events/sanja-matsuri.jpg", Description = "desEv8" },
            new Event { Id = 10, Name = "Global village 25th season", Venue = Venues[11], Date = new DateTime(2021, 04, 04), Banner = "events/global-village.jpg", Description = "desEv9" }
        };

         private static readonly List<Ticket> Tickets = new List<Ticket>
        {
            new Ticket { Id = 1, Event = Events[0], Price = 15, Seller = Users[0], Status = (byte)TicketStatuses.selling },
            new Ticket { Id = 2, Event = Events[0], Price = 20, Seller = Users[0], Status = (byte)TicketStatuses.sold },
            new Ticket { Id = 3, Event = Events[1], Price = 35, Seller = Users[0], Status = (byte)TicketStatuses.waiting },
            new Ticket { Id = 4, Event = Events[2], Price = 100, Seller = Users[1], Status = (byte)TicketStatuses.selling },
            new Ticket { Id = 5, Event = Events[2], Price = 105, Seller = Users[1], Status = (byte)TicketStatuses.waiting },
            new Ticket { Id = 6, Event = Events[2], Price = 170, Seller = Users[1], Status = (byte)TicketStatuses.waiting },
            new Ticket { Id = 7, Event = Events[3], Price = 10, Seller = Users[0], Status = (byte)TicketStatuses.selling },
            new Ticket { Id = 8, Event = Events[3], Price = 6, Seller = Users[0], Status = (byte)TicketStatuses.sold },
            new Ticket { Id = 9, Event = Events[4], Price = 200, Seller = Users[2], Status = (byte)TicketStatuses.selling },
            new Ticket { Id = 10, Event = Events[4], Price = 240, Seller = Users[2], Status = (byte)TicketStatuses.sold },
            new Ticket { Id = 11, Event = Events[4], Price = 260, Seller = Users[2], Status = (byte)TicketStatuses.sold },
            new Ticket { Id = 12, Event = Events[4], Price = 210, Seller = Users[2], Status = (byte)TicketStatuses.waiting },
            new Ticket { Id = 13, Event = Events[5], Price = 70, Seller = Users[2], Status = (byte)TicketStatuses.selling },
            new Ticket { Id = 14, Event = Events[5], Price = 90, Seller = Users[2], Status = (byte)TicketStatuses.sold },
            new Ticket { Id = 15, Event = Events[5], Price = 150, Seller = Users[2], Status = (byte)TicketStatuses.selling },
            new Ticket { Id = 16, Event = Events[5], Price = 200, Seller = Users[2], Status = (byte)TicketStatuses.waiting },
            new Ticket { Id = 17, Event = Events[6], Price = 800, Seller = Users[2], Status = (byte)TicketStatuses.sold },
            new Ticket { Id = 18, Event = Events[6], Price = 750, Seller = Users[1], Status = (byte)TicketStatuses.selling },
            new Ticket { Id = 19, Event = Events[6], Price = 780, Seller = Users[2], Status = (byte)TicketStatuses.sold },
            new Ticket { Id = 20, Event = Events[7], Price = 130, Seller = Users[2], Status = (byte)TicketStatuses.sold },
            new Ticket { Id = 21, Event = Events[7], Price = 200, Seller = Users[2], Status = (byte)TicketStatuses.selling },
            new Ticket { Id = 22, Event = Events[8], Price = 80, Seller = Users[2], Status = (byte)TicketStatuses.selling },
            new Ticket { Id = 23, Event = Events[9], Price = 1500, Seller = Users[2], Status = (byte)TicketStatuses.waiting }
        };

        private static readonly List<Order> Orders = new List<Order>
        {
            new Order { Id = 1, Buyer = Users[0], Status = (byte)OrderStatuses.confirmed, Ticket = Tickets[0], TrackNumber = "SN53245AB21" },
            new Order { Id = 2, Buyer = Users[1], Status = (byte)OrderStatuses.rejected, Ticket = Tickets[2], TrackNumber = "SN34535AB98" },
            new Order { Id = 3, Buyer = Users[2], Status = (byte)OrderStatuses.rejected, Ticket = Tickets[6], TrackNumber = "SN18175AB74" },
            new Order { Id = 4, Buyer = Users[1], Status = (byte)OrderStatuses.waiting, Ticket = Tickets[9], TrackNumber = "SN77756AB13" },
            new Order { Id = 5, Buyer = Users[1], Status = (byte)OrderStatuses.confirmed, Ticket = Tickets[12], TrackNumber = "SN22467AB21" },
            new Order { Id = 6, Buyer = Users[0], Status = (byte)OrderStatuses.waiting, Ticket = Tickets[15], TrackNumber = "SN34563AB67" },
            new Order { Id = 7, Buyer = Users[1], Status = (byte)OrderStatuses.rejected, Ticket = Tickets[18], TrackNumber = "SN34442AB67" },
            new Order { Id = 8, Buyer = Users[0], Status = (byte)OrderStatuses.waiting, Ticket = Tickets[20], TrackNumber = "SN53245AB76" },
            new Order { Id = 9, Buyer = Users[2], Status = (byte)OrderStatuses.confirmed, Ticket = Tickets[4], TrackNumber = "SN98762AB21" },
            new Order { Id = 10, Buyer = Users[0], Status = (byte)OrderStatuses.rejected, Ticket = Tickets[22], TrackNumber = "SN23421AB33" }
        };



        public async Task SeedDataAsync()
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Cities.Any())
            {
                await context.Cities.AddRangeAsync(Cities);
            }

            if (!context.Users.Any())
            {
                await context.Users.AddRangeAsync(Users);
            }

            if (!context.Venues.Any())
            {
                await context.Venues.AddRangeAsync(Venues);
            }

            if (!context.Events.Any())
            {
                await context.Events.AddRangeAsync(Events);
            }

            if (!context.Tickets.Any())
            {
                await context.Tickets.AddRangeAsync(Tickets);
            }

            if (!context.Orders.Any())
            {
                await context.Orders.AddRangeAsync(Orders);
            }

            await context.SaveChangesAsync();
        }
    }
}
