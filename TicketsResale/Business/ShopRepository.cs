using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketsResale.Business.Models;
using TicketsResale.Models;

namespace TicketsResale.Business
{
    public sealed class ShopRepository
    {
        private List<User> Users;

        private List<City> Cities;

        private List<Venue> Venues;

        private List<Event> Events;

        private List<Order> Orders;

        private List<Ticket> Tickets;

        EventTickets eventTickets = new EventTickets();

        private readonly IStringLocalizer<ShopRepository> Localizer;

        public ShopRepository(IStringLocalizer<ShopRepository> localizer)
        {
            this.Localizer = localizer;

            Users = new List<User>
                {
                    new User { Id = 0, FirstName = Localizer["Alexey"], LastName = Localizer["Robinson"], Address = Localizer["adrUser0"], Localization = "rus", PhoneNumber = "228228", UserName = "admin", Password = "admin", Role = "Administrator" },
                    new User { Id = 1, FirstName = Localizer["Jominez"], LastName = Localizer["Maxwell"], Address = Localizer["adrUser1"], Localization = "spa", PhoneNumber = "345124", UserName = "user", Password = "user", Role = "User" },
                    new User { Id = 2, FirstName = Localizer["Alibaba"], LastName = Localizer["Bestseller"], Address = Localizer["adrUser2"], Localization = "eng", PhoneNumber = "777777", UserName = "seller", Password = "seller", Role = "User" },
            };

            Cities = new List<City>
                {
                    new City { Id = 0, Name = Localizer["Grodno"] },
                    new City { Id = 1, Name = Localizer["Minsk"] },
                    new City { Id = 2, Name = Localizer["Barselona"] },
                    new City { Id = 3, Name = Localizer["NewYork"] },
                    new City { Id = 4, Name = Localizer["Tokyo"] },
                    new City { Id = 5, Name = Localizer["Dubai"] },
                };

            Venues = new List<Venue> 
                {
                    new Venue { Id = 0, Name = Localizer["Sovetskaya sq."], Address = Localizer["Sovetskaya sq."], City = Cities[0] },
                    new Venue { Id = 1, Name = Localizer["Pyshki forest park"], Address = Localizer["adrVen1"], City = Cities[0] },
                    new Venue { Id = 2, Name = Localizer["Independense sq."], Address = Localizer["adrVen2"], City = Cities[1] },
                    new Venue { Id = 3, Name = Localizer["Central botanical garden"], Address = Localizer["adrVen3"], City = Cities[1] },
                    new Venue { Id = 4, Name = Localizer["Park Forum"], Address = Localizer["adrVen4"], City = Cities[2] },
                    new Venue { Id = 5, Name = Localizer["Igulada"], Address = Localizer["adrVen5"], City = Cities[2] },
                    new Venue { Id = 6, Name = Localizer["Broadway Majestic Theatre"], Address = Localizer["adrVen6"], City = Cities[3] },
                    new Venue { Id = 7, Name = Localizer["Radio city Rockettes"], Address = Localizer["adrVen7"], City = Cities[3] },
                    new Venue { Id = 8, Name = Localizer["Fukagawa Sakura"], Address = Localizer["adrVen8"], City = Cities[4] },
                    new Venue { Id = 9, Name = Localizer["Sanja Matsuri"], Address = Localizer["adrVen9"], City = Cities[4] },
                    new Venue { Id = 10, Name = Localizer["Dubai world trade center"], Address = Localizer["adrVen10"], City = Cities[5] },
                    new Venue { Id = 11, Name = Localizer["Global village"], Address = Localizer["Global village"], City = Cities[5] },
                };

            Events = new List<Event>
                {       
                        new Event { Id = 0, Name = Localizer["Festival of national cultures 2021"], Venue = Venues[0], Date = new DateTime(2021, 06, 05), Banner = "events/fnk-grodno.jpg",  Description = Localizer["desEv0"] },
                        new Event { Id = 1, Name = Localizer["Color Fest 2020"], Venue = Venues[1], Date = new DateTime(2020, 09, 22), Banner = "events/colorfest-grodno.jpg", Description = Localizer["desEv1"] },
                        new Event { Id = 2, Name = Localizer["Primavera Sound Barselona"], Venue = Venues[4], Date = new DateTime(2021, 06, 14), Banner = "events/primavera-barsa.jpg", Description = Localizer["desEv2"] },
                        new Event { Id = 3, Name = Localizer["Magic garden. Festival of Music and Theater"], Venue = Venues[3], Date = new DateTime(2020, 09, 14), Banner = "events/botanic-minsk.jpg", Description= Localizer["desEv3"]},
                        new Event { Id = 4, Name = Localizer["BALLOON FESTIVAL"], Venue = Venues[5], Date = new DateTime(2021, 07, 11), Banner = "events/baloon-igulada.jpg", Description = Localizer["desEv4"] },
                        new Event { Id = 5, Name = Localizer["The Phantom of the Opera"], Venue = Venues[6], Date = new DateTime(2020, 09, 25), Banner = "events/majestic-newyork.jpg", Description = Localizer["desEv5"]},
                        new Event { Id = 6, Name = Localizer["ROCKETTES CHRISTMAS SPECTACULAR 2021"], Venue = Venues[7], Date = new DateTime(2020, 12, 24), Banner = "events/rockettes-newyork.jpg", Description = Localizer["desEv6"]},
                        new Event { Id = 7, Name = Localizer["Edo Fukagawa Sakura Festival"], Venue = Venues[8], Date = new DateTime(2021, 04, 05), Banner = "events/fukagawa-sakura.png", Description = Localizer["desEv7"]},
                        new Event { Id = 8, Name = Localizer["Sanja Matsuri Festival"], Venue = Venues[9], Date = new DateTime(2020, 10, 17), Banner = "events/sanja-matsuri.jpg", Description = Localizer["desEv8"] },
                        new Event { Id = 9, Name = Localizer["Global village 25th season"], Venue = Venues[11], Date = new DateTime(2021, 04, 04), Banner = "events/global-village.jpg", Description = Localizer["desEv9"] },
                };

            Tickets = new List<Ticket>
                {
                        new Ticket { Id = 0, Event = Events[0], Price = 15, Seller = Users[0], Status = TicketStatuses.selling},
                        new Ticket { Id = 1, Event = Events[0], Price = 20, Seller = Users[0], Status = TicketStatuses.sold},
                        new Ticket { Id = 2, Event = Events[1], Price = 35, Seller = Users[0], Status = TicketStatuses.waiting},
                        new Ticket { Id = 3, Event = Events[2], Price = 100, Seller = Users[1], Status = TicketStatuses.selling},
                        new Ticket { Id = 4, Event = Events[2], Price = 105, Seller = Users[1], Status = TicketStatuses.waiting},
                        new Ticket { Id = 5, Event = Events[2], Price = 170, Seller = Users[1], Status = TicketStatuses.waiting},
                        new Ticket { Id = 6, Event = Events[3], Price = 10, Seller = Users[0], Status = TicketStatuses.selling},
                        new Ticket { Id = 7, Event = Events[3], Price = 6, Seller = Users[0], Status = TicketStatuses.sold},
                        new Ticket { Id = 8, Event = Events[4], Price = 200, Seller = Users[2], Status = TicketStatuses.selling},
                        new Ticket { Id = 9, Event = Events[4], Price = 240, Seller = Users[2], Status = TicketStatuses.sold},
                        new Ticket { Id = 10, Event = Events[4], Price = 260, Seller = Users[2], Status = TicketStatuses.sold},
                        new Ticket { Id = 11, Event = Events[4], Price = 210, Seller = Users[2], Status = TicketStatuses.waiting},
                        new Ticket { Id = 12, Event = Events[5], Price = 70, Seller = Users[2], Status = TicketStatuses.selling},
                        new Ticket { Id = 13, Event = Events[5], Price = 90, Seller = Users[2], Status = TicketStatuses.sold},
                        new Ticket { Id = 14, Event = Events[5], Price = 150, Seller = Users[2], Status = TicketStatuses.selling},
                        new Ticket { Id = 15, Event = Events[5], Price = 200, Seller = Users[2], Status = TicketStatuses.waiting},
                        new Ticket { Id = 16, Event = Events[6], Price = 800, Seller = Users[2], Status = TicketStatuses.sold},
                        new Ticket { Id = 17, Event = Events[6], Price = 750, Seller = Users[1], Status = TicketStatuses.selling},
                        new Ticket { Id = 18, Event = Events[6], Price = 780, Seller = Users[2], Status = TicketStatuses.sold},
                        new Ticket { Id = 19, Event = Events[7], Price = 130, Seller = Users[2], Status = TicketStatuses.sold},
                        new Ticket { Id = 20, Event = Events[7], Price = 200, Seller = Users[2], Status = TicketStatuses.selling},
                        new Ticket { Id = 21, Event = Events[8], Price = 80, Seller = Users[2], Status = TicketStatuses.selling},
                        new Ticket { Id = 22, Event = Events[9], Price = 1500, Seller = Users[2], Status = TicketStatuses.waiting},
                };

            Orders = new List<Order>
                {
                        new Order { Id = 0, Buyer = Users[0], Status = OrderStatuses.confirmed, Ticket = Tickets[0], TrackNumber = "SN53245AB21"},
                        new Order { Id = 1, Buyer = Users[1], Status = OrderStatuses.rejected, Ticket = Tickets[2], TrackNumber = "SN34535AB98"},
                        new Order { Id = 2,  Buyer = Users[2], Status = OrderStatuses.rejected, Ticket = Tickets[6], TrackNumber = "SN18175AB74"},
                        new Order { Id = 3,  Buyer = Users[1], Status = OrderStatuses.waiting, Ticket = Tickets[9], TrackNumber = "SN77756AB13"},
                        new Order { Id = 4,  Buyer = Users[1], Status = OrderStatuses.confirmed, Ticket = Tickets[12], TrackNumber = "SN22467AB21"},
                        new Order { Id = 5, Buyer = Users[0], Status = OrderStatuses.waiting, Ticket = Tickets[15], TrackNumber = "SN34563AB67"},
                        new Order { Id = 6, Buyer = Users[1], Status = OrderStatuses.rejected, Ticket = Tickets[18], TrackNumber = "SN34442AB67"},
                        new Order { Id = 7, Buyer = Users[0], Status = OrderStatuses.waiting, Ticket = Tickets[20], TrackNumber = "SN53245AB76"},
                        new Order { Id = 8, Buyer = Users[2], Status = OrderStatuses.confirmed, Ticket = Tickets[4], TrackNumber = "SN98762AB21"},
                        new Order { Id = 9,  Buyer = Users[0], Status = OrderStatuses.rejected, Ticket = Tickets[22], TrackNumber = "SN23421AB33"},
                };
        }

        public Ticket[] GetTickets(TicketStatuses status, string userName)
        {
            var chosenTickets = new List<Ticket>();
            for (int i = 0; i < Tickets.Count; i++)
            {
                if ((Tickets[i].Status == status) && (Tickets[i].Seller.UserName == userName))
                    chosenTickets.Add(Tickets[i]);
            }

            return chosenTickets.ToArray();
        }

        public EventTickets GetEventWithTickets(int eventId)
        {
            var chosenEvent = Events.Where(e => e.Id == eventId).Select(e => e).FirstOrDefault();
            var chosenTickets = Tickets.Where(t => t.Event.Id == eventId).Select(t => t).AsEnumerable().ToList();

            Dictionary<Event, List<Ticket>> dic = new Dictionary<Event, List<Ticket>>();
            dic.Add(chosenEvent, chosenTickets);

            eventTickets.eventTickets = dic;

            return eventTickets;
        }

        public User[] GetUsers()
        {
            return Users.ToArray();
        }

        public List<User> GetUsersList()
        {
            return Users.ToList();
        }

        public City[] GetCities()
        {
            return Cities.ToArray();
        }

        public Venue[] GetVenues()
        {
            return Venues.ToArray();
        }

        public Event[] GetEvents()
        {
            return Events.ToArray();
        }

        public Ticket[] GetTickets()
        {
            return Tickets.ToArray();
        }

        public Order[] GetOrders()
        {
            return Orders.ToArray();
        }

        public City GetCityById(int id)
        {
            return Cities.FirstOrDefault(p => p.Id == id);
        }

        public User GetUserByName(string name)
        {
            return Users.FirstOrDefault(p => p.UserName == name);
        }

        public Venue GetVenueById(int id)
        {
            return Venues.FirstOrDefault(p => p.Id == id);
        }

        public Event GetEventById(int id)
        {
            return Events.FirstOrDefault(p => p.Id == id);
        }

        public Ticket GetTicketById(int id)
        {
            return Tickets.FirstOrDefault(p => p.Id == id);
        }

        public Order GetOrderById(int id)
        {
            return Orders.FirstOrDefault(p => p.Id == id);
        }
    }
}
