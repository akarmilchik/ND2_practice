using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale.Test
{
    [TestFixture]
    public class SQLiteServicesTest
    {
        private List<City> Cities;
        private  List<StoreUser> Users;
        private  List<IdentityRole> Roles;
        private List<Venue> Venues;
        private  List<EventCategory> EventCategories;
        private List<Event> Events;
        private List<Ticket> Tickets;
        private List<Order> Orders;
        ConnectionFactory factory;
        StoreContext context;
        City UpdatedCity;
        List<Ticket> SomeTickets;
        List<int> PagesList;
        Event UpdatedEvent;
        Venue UpdatedVenue;
        Order UpdatedOrder;
        Ticket UpdatedTicket;
        List<IdentityUserRole<string>> UsersRoles;

        [SetUp]
        public void Setup()
        {
            factory = new ConnectionFactory();

            Cities = new List<City>()
            {
                new City { Name = "Grodno" },
                new City { Name = "Minsk" },
                new City { Name = "Barselona" },
                new City { Name = "NewYork" },
                new City { Name = "Tokyo" },
                new City { Name = "Dubai" }
            };

            UpdatedCity = new City { Name = "Kamenets" };

            PagesList = new List<int> { 1, 2, 3, 4 };

            Users = new List<StoreUser>()
            {
                new StoreUser { FirstName = "Alexey", LastName = "Karm", Address = "15, Kosmonavtov Av., Grodno, BLR", Localization = Localizations.rus, PhoneNumber = "228228", UserName = "alexey.karm@mail.ru", Email = "alexey.karm@mail.ru" },
                new StoreUser { FirstName = "Jominez", LastName = "Maxwell", Address = "132/1, Sunlight Av., Barselona, SPA", Localization = Localizations.bel, PhoneNumber = "345124", UserName = "j@mail.ru", Email = "j@mail.ru" },
                new StoreUser { FirstName = "Alibaba", LastName = "Bestseller", Address = "6/1, 123 Av., New York, USA", Localization = Localizations.eng, PhoneNumber = "777777", UserName = "a@mail.ru", Email = "a@mail.ru" }
            };

            Roles = new List<IdentityRole>()
            {
                new IdentityRole { Name = "Administrator" },
                new IdentityRole { Name = "User" }
            };

            UsersRoles = new List<IdentityUserRole<string>> { new IdentityUserRole<string> { RoleId = Roles[0].Id, UserId = Users[0].Id }, new IdentityUserRole<string> { RoleId = Roles[1].Id, UserId = Users[1].Id } };

            Venues = new List<Venue>()
            {
                new Venue { Name = "Sovetskaya sq.", Address = "Sovetskaya sq.", City = Cities[0] },
                new Venue { Name = "Pyshki forest park", Address = "10, Festivalnaya st.", City = Cities[0] },
                new Venue { Name = "Independense sq.", Address = "15, Independense av.", City = Cities[1] },
                new Venue { Name = "Central botanical garden", Address = "2b, Surganova st.", City = Cities[1] },
                new Venue { Name = "Park Forum", Address = "Carrer de La Pau, 12, Sant Adria De Besos", City = Cities[2] },
                new Venue { Name = "Igulada", Address = "Igulada city", City = Cities[2] },
                new Venue { Name = "Broadway Majestic Theatre", Address = "245 West, 44th Street", City = Cities[3] },
                new Venue { Name = "Radio city Rockettes", Address = "1260 av. btw 50th and 51st st.", City = Cities[3] },
                new Venue { Name = "Fukagawa Sakura", Address = "2-1-8 Monzennakacho, Koto 135-0048 Perfecture", City = Cities[4] },
                new Venue { Name = "Sanja Matsuri", Address = "2-3-1 Asakusa Shrine, Asakusa, Taito 111-0032 Perfecture", City = Cities[4] },
                new Venue { Name = "Dubai world trade center", Address = "Trade Centre 2", City = Cities[5] },
                new Venue { Name = "Global village", Address = "Global village", City = Cities[5] }
            };

            UpdatedVenue = new Venue { Name = "Some name of venue", Address = "Sovetskaya sq.", City = Cities[0] };

            EventCategories = new List<EventCategory>()
            {
                new EventCategory { Name = "Street" },
                new EventCategory { Name = "Sports" },
                new EventCategory { Name = "Festivals" },
                new EventCategory { Name = "Plays" }
            };

            Events = new List<Event>()
            {
                new Event { Name = "Festival of national cultures 2021", Venue = Venues[0], EventCategory = EventCategories[2], Date = new DateTime(2021, 06, 05), Banner = "events/fnk-grodno.jpg", Description = "The <strong>Republican Festival of National Cultures</strong> is a holiday of folklore colors of various peoples living in <em>Belarus</em>. Representatives of <strong>36</strong> nationalities participate in the festival, the attendance of the festival in <strong>2018</strong> was <strong>270</strong> thousand people." },
                new Event { Name = "Color Fest 2020", Venue = Venues[1], EventCategory = EventCategories[0], Date = new DateTime(2020, 09, 22), Banner = "events/colorfest-grodno.jpg", Description = "Favorite dance music, tons of natural bright colors and fun contests are just a part of what awaits the guests of the  <strong>Festival of Colors</strong>. An atmosphere of happiness and carelessness will reign here, thousands of smiles of bright faces and warm embraces of colorful merry fellows will create an indescribable feeling of unity that will overwhelm your head!" },
                new Event { Name = "Primavera Sound Barselona", Venue = Venues[4], EventCategory = EventCategories[2], Date = new DateTime(2021, 06, 14), Banner = "events/primavera-barsa.jpg", Description = "The <strong>Primavera Sound Festival</strong> program <em>(Spanish + English \"Sounds of Spring\")</em> in Barcelona usually shines with the most famous bands and musicians from <ins>around the world.</ins> The event takes place at the end of May or at the end of June and brings together about <strong>100,000</strong> music lovers" },
                new Event { Name = "Magic garden. Festival of Music and Theater", Venue = Venues[3], EventCategory = EventCategories[3], Date = new DateTime(2020, 09, 14), Banner = "events/botanic-minsk.jpg", Description = "Travel back in time and find yourself on a weekend in ... <strong>Ancient Greece</strong>? Easy! On <ins>September 14-15</ins>, the <strong>Central Botanical Garden</strong> will turn into a small Hellas. A real immersion in the land of ancient gods, the cradle of sciences, culture and art awaits you." },
                new Event { Name = "BALLOON FESTIVAL", Venue = Venues[5], EventCategory = EventCategories[0], Date = new DateTime(2021, 07, 11), Banner = "events/baloon-igulada.jpg", Description = "From 11 to 14 July, the city of <strong>Igualada</strong>, an hour's drive from Barcelona, will host a hot air balloon festival. Flying in a hot air balloon is romantic and beautiful, and when there are about twenty bright balloons next to you in the sky, it is mesmerizing. In addition, the Igualada festival is considered the largest in <em>Europe</em>." },
                new Event { Name = "The Phantom of the Opera", Venue = Venues[6], EventCategory = EventCategories[3], Date = new DateTime(2020, 09, 25), Banner = "events/majestic-newyork.jpg", Description = "Based on the <mark>1910</mark> horror novel by <strong>Gaston Leroux</strong>, which has been adapted into countless films, <strong>The Phantom of the Opera</strong> follows a deformed composer who haunts the grand Paris Opera House. Sheltered from the outside world in an underground cavern, the lonely, romantic man tutors and composes operas for <em>Christine</em>, a gorgeous young soprano star-to-be. As <em>Christine’s</em> star rises, and a handsome suitor from her past enters the picture, the Phantom grows mad, terrorizing the opera house owners and company with his murderous ways. Still, <em>Christine</em> finds herself drawn to the mystery man." },
                new Event { Name = "ROCKETTES CHRISTMAS SPECTACULAR 2021", Venue = Venues[7], EventCategory = EventCategories[3], Date = new DateTime(2020, 12, 24), Banner = "events/rockettes-newyork.jpg", Description = "Few things are as emblematic of <strong>New York City</strong> or the holiday season as <strong>The Rockettes</strong>, a precision dance company that’s been performing at <em>Radio City Music Hall</em> in Manhattan since <mark>1932</mark>. Best known for their eye-high leg-kicking routine, in which dozens of dancers seem to move as one in perfect unison — as well as their delightful annual holiday event <strong>The Radio City Christmas Spectacular</strong> — The Rockettes’ mixture of modern dance and classic ballet has been enjoyed by millions upon millions of people, thanks in part to the troupe’s touring company that brings the fun to theaters all across the country." },
                new Event { Name = "Edo Fukagawa Sakura Festival", Venue = Venues[8], EventCategory = EventCategories[1], Date = new DateTime(2021, 04, 05), Banner = "events/fukagawa-sakura.png", Description = "<strong>Fukagawa’s annual sakura matsuri</strong> takes place along the <em>Ooyokogawa</em> river, with a seemingly neverending trail of cherry blossoms in full bloom. Out of all the sakura festivals <strong>Tokyo</strong> has to offer, this is the only place you can admire the beautiful scenery from below while cruising in Japanese-style boats. A pair of shamisen players will also be travelling onboard while performing ‘Shinnai-nagashi’, a traditional Japanese tune that will transport you back to the Edo period (1603-1868)." },
                new Event { Name = "Sanja Matsuri Festival", Venue = Venues[9], EventCategory = EventCategories[0], Date = new DateTime(2020, 10, 17), Banner = "events/sanja-matsuri.jpg", Description = "<strong>The Sanja Festival</strong> (<em>三社祭, Sanja Matsuri</em>) is an annual festival in the <strong>Asakusa</strong> district that usually takes place over the third full weekend in May. It is held in celebration of the three founders of Sensoji Temple, who are enshrined in Asakusa Shrine next door to the temple. Nearly two million people visit Asakusa over the three days of the festival, making it one of Tokyo's most popular festivals." },
                new Event { Name = "Global village 25th season", Venue = Venues[11], EventCategory = EventCategories[2], Date = new DateTime(2021, 04, 04), Banner = "events/global-village.jpg", Description = "<strong>The World Village Dubai</strong> will feature a total of <mark>78</mark> countries. With, new additions- Azerbaijan and Korea, the place promises to be too much fun! The countries will have 26 pavilions that will have cuisines, shows and products from all around the globe." }
            };

            UpdatedEvent = new Event { Name = "Some name of event", Venue = Venues[0], EventCategory = EventCategories[2], Date = new DateTime(2021, 06, 05), Banner = "events/fnk-grodno.jpg", Description = "The <strong>Republican Festival of National Cultures</strong> is a holiday of folklore colors of various peoples living in <em>Belarus</em>. Representatives of <strong>36</strong> nationalities participate in the festival, the attendance of the festival in <strong>2018</strong> was <strong>270</strong> thousand people." };

            Tickets = new List<Ticket>()
            {
                new Ticket { Event = Events[0], Price = 15, Seller = Users[0], Status = TicketStatuses.selling },
                new Ticket { Event = Events[0], Price = 20, Seller = Users[0], Status = TicketStatuses.sold },
                new Ticket { Event = Events[1], Price = 35, Seller = Users[0], Status = TicketStatuses.waiting },
                new Ticket { Event = Events[2], Price = 100, Seller = Users[1], Status = TicketStatuses.selling },
                new Ticket { Event = Events[2], Price = 105, Seller = Users[1], Status = TicketStatuses.waiting },
                new Ticket { Event = Events[2], Price = 170, Seller = Users[1], Status = TicketStatuses.waiting },
                new Ticket { Event = Events[3], Price = 10, Seller = Users[0], Status = TicketStatuses.selling },
                new Ticket { Event = Events[3], Price = 6, Seller = Users[0], Status = TicketStatuses.sold },
                new Ticket { Event = Events[4], Price = 200, Seller = Users[2], Status = TicketStatuses.selling },
                new Ticket { Event = Events[4], Price = 240, Seller = Users[2], Status = TicketStatuses.sold },
                new Ticket { Event = Events[4], Price = 260, Seller = Users[2], Status = TicketStatuses.sold },
                new Ticket { Event = Events[4], Price = 210, Seller = Users[0], Status = TicketStatuses.waiting },
                new Ticket { Event = Events[5], Price = 70, Seller = Users[2], Status = TicketStatuses.selling },
                new Ticket { Event = Events[5], Price = 90, Seller = Users[2], Status = TicketStatuses.sold },
                new Ticket { Event = Events[5], Price = 150, Seller = Users[2], Status = TicketStatuses.selling },
                new Ticket { Event = Events[5], Price = 200, Seller = Users[2], Status = TicketStatuses.waiting },
                new Ticket { Event = Events[6], Price = 800, Seller = Users[2], Status = TicketStatuses.sold },
                new Ticket { Event = Events[6], Price = 750, Seller = Users[1], Status = TicketStatuses.selling },
                new Ticket { Event = Events[6], Price = 780, Seller = Users[2], Status = TicketStatuses.sold },
                new Ticket { Event = Events[7], Price = 130, Seller = Users[2], Status = TicketStatuses.sold },
                new Ticket { Event = Events[7], Price = 200, Seller = Users[2], Status = TicketStatuses.selling },
                new Ticket { Event = Events[8], Price = 80, Seller = Users[2], Status = TicketStatuses.selling },
                new Ticket { Event = Events[9], Price = 1500, Seller = Users[2], Status = TicketStatuses.waiting }
            };

            UpdatedTicket = new Ticket { Event = Events[0], Price = 999, Seller = Users[0], Status = TicketStatuses.selling }; 
            SomeTickets = new List<Ticket>() { Tickets[0], Tickets[5], Tickets[9] };

            //Orders of Users
            Orders = new List<Order>()
            {
                new Order { Buyer = Users[2], Status = OrderStatuses.confirmed, Ticket = Tickets[0], TrackNumber = "SN53245AB21" },
                new Order { Buyer = Users[1], Status = OrderStatuses.rejected, Ticket = Tickets[2], TrackNumber = "just simple" },
                new Order { Buyer = Users[2], Status = OrderStatuses.rejected, Ticket = Tickets[6], TrackNumber = "test attempt" },
                new Order { Buyer = Users[1], Status = OrderStatuses.waiting, Ticket = Tickets[9], TrackNumber = "" },
                new Order { Buyer = Users[0], Status = OrderStatuses.confirmed, Ticket = Tickets[12], TrackNumber = "SN22467AB21" },
                new Order { Buyer = Users[0], Status = OrderStatuses.waiting, Ticket = Tickets[15], TrackNumber = "" },
                new Order { Buyer = Users[1], Status = OrderStatuses.rejected, Ticket = Tickets[18], TrackNumber = "don't liked" },
                new Order { Buyer = Users[2], Status = OrderStatuses.waiting, Ticket = Tickets[20], TrackNumber = "" },
                new Order { Buyer = Users[0], Status = OrderStatuses.confirmed, Ticket = Tickets[4], TrackNumber = "SN98762AB21" },
                new Order { Buyer = Users[1], Status = OrderStatuses.rejected, Ticket = Tickets[22], TrackNumber = "too small" }
            };

            UpdatedOrder = new Order { Buyer = Users[2], Status = OrderStatuses.confirmed, Ticket = Tickets[0], TrackNumber = "TN12412UR54" };
        }

        [Test]
        public async Task GetCities_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Cities.AddRangeAsync(Cities); 
          
            await context.SaveChangesAsync();

            var service = new CitiesService(context);

            // Act
            var cities = await service.GetCities();

            // Assert
            Assert.IsNotEmpty(cities);
        }

        [Test]
        public async Task GetCities_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new CitiesService(context);

            // Act
            var cities = await service.GetCities();

            // Assert
            Assert.IsEmpty(cities);
        }

        [Test]
        public async Task GetCitiesWithPagination_CorrectDataProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Cities.AddRangeAsync(Cities);

            await context.SaveChangesAsync();

            var service = new CitiesService(context);

            // Act
            var cities = await service.GetCities(1, 5);

            // Assert
            Assert.IsNotEmpty(cities);
        }

        [Test]
        public async Task GetCitiesWithPagination_IncorrectDataProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Cities.AddRangeAsync(Cities);

            await context.SaveChangesAsync();

            var service = new CitiesService(context);

            // Act
            var citiesByPageSize = await service.GetCities(1, 0);
            var citiesByPage = await service.GetCities(0, 5);
            var citiesByPageAndPageSize = await service.GetCities(0, 0);

            // Assert
            Assert.IsNotEmpty(citiesByPageSize);
            Assert.IsNotEmpty(citiesByPage);
            Assert.IsNotEmpty(citiesByPageAndPageSize);
        }

        [Test]
        public async Task GetCitiesPages_CorrectDataProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Cities.AddRangeAsync(Cities);

            await context.SaveChangesAsync();

            var service = new CitiesService(context);

            // Act
            var pages = service.GetCitiesPages(2);

            // Assert
            Assert.IsNotEmpty(pages);
        }

        [Test]
        public async Task GetCitiesPages_IncorrectDataProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Cities.AddRangeAsync(Cities);

            await context.SaveChangesAsync();

            var service = new CitiesService(context);

            // Act
            var pages = service.GetCitiesPages(0);

            // Assert
            Assert.IsNotEmpty(pages);
        }

        [Test]
        public void GetNearPages_CorrectDataProvided_CorrectTotalReturned()
        {
            // Arrange
            var service = new CitiesService(context);

            // Act
            var pages = service.GetNearPages(PagesList, 2);

            // Assert
            Assert.IsNotEmpty(pages);
        }

        [Test]
        public void GetNearPages_IncorrectDataProvided_CorrectTotalReturned()
        {
            // Arrange
            var service = new CitiesService(context);

            // Act
            var pages = service.GetNearPages(PagesList, 0);

            // Assert
            Assert.IsNotEmpty(pages);
        }

        [Test]
        public async Task GetCityById_CorrectCityProvided_CorrecCityReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Cities.AddRangeAsync(Cities);
            
            await context.SaveChangesAsync();

            var service = new CitiesService(context);

            // Act
            var resultCity = await service.GetCityById(3);

            // Assert
            Assert.IsNotNull(resultCity);
            Assert.AreEqual("Barselona", resultCity.Name);
        }

        [Test]
        public async Task GetCityById_NullProvided_NullReturned()
        {
            // Arrange
            var service = new CitiesService(context);

            // Act
            var city = await service.GetCityById(999);

            // Assert
            Assert.IsNull(city);
           
        }

        [Test]
        public async Task GetCityNameById_CorrectCityProvided_CorrecCityReturned()
        {
            // Arrange
            await context.Cities.AddRangeAsync(Cities);
            
            await context.SaveChangesAsync();

            var service = new CitiesService(context);

            // Act
            var name = await service.GetCityNameById(3);

            // Assert
            Assert.AreNotSame("", name);
            Assert.AreEqual("Barselona", name);
        }

        [Test]
        public async Task GetCityNameById_NullProvided_NullReturned()
        {
            // Arrange
            var service = new CitiesService(context);

            // Act
            var name = await service.GetCityNameById(999);

            // Assert
            Assert.IsNull(name);

        }

        [Test]
        public async Task AddCityToDb_CorrectAdding()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new CitiesService(context);

            // Act
            await service.AddCityToDb(Cities[1]);

            var city = await context.Cities.FirstOrDefaultAsync();

            // Assert
            Assert.AreEqual("Minsk", city.Name);
        }

        [Test]
        public async Task UpdCityToDb_CorrectUpdate()
        {
            // Arrange
            await context.Cities.AddRangeAsync(Cities);

            await context.SaveChangesAsync();

            var service = new CitiesService(context);
            // Act
            await service.UpdCityToDb(UpdatedCity);

            var resultCity = await context.Cities.Where(c => c.Name == UpdatedCity.Name).FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(resultCity);
        }

        [Test]
        public async Task RemoveCityFromDb_CorrectRemove()
        {
            // Arrange
            await context.Cities.AddRangeAsync(Cities);
            
            await context.SaveChangesAsync();

            var service = new CitiesService(context);

            // Act
            await service.RemoveCityFromDb(Cities[3]);

            // Assert
            Assert.AreEqual(5, context.Cities.Count());
            Assert.IsNull(await context.Cities.Where(c => c.Name == "NewYork").FirstOrDefaultAsync());
        }

        [Test]
        public async Task GetEvents_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.EventCategories.AddRangeAsync(EventCategories);
            await context.Cities.AddRangeAsync(Cities);
            await context.Venues.AddRangeAsync(Venues);
            await context.Events.AddRangeAsync(Events);
            
            await context.SaveChangesAsync();

            var service = new EventsService(context);

            // Act
            var events = await service.GetEvents();

            // Assert
            Assert.IsNotNull(events);
            Assert.IsTrue(events.Count > 0);
        }

        [Test]
        public async Task GetEvents_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new EventsService(context);

            // Act
            var events = await service.GetEvents();

            // Assert
            Assert.IsEmpty(events);
        }

        [Test]
        public async Task GetEventsByTickets_CorrectListOfTicketsProvided_CorrectListOfEventsReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);
            await context.EventCategories.AddRangeAsync(EventCategories);
            await context.Cities.AddRangeAsync(Cities);
            await context.Events.AddRangeAsync(Events);
            await context.Tickets.AddRangeAsync(Tickets);
            await context.SaveChangesAsync();

            var service = new EventsService(context);

            // Act
            var events = await service.GetEventsByTickets(SomeTickets);

            // Assert
            Assert.IsNotNull(events);
            Assert.IsTrue(events.Count > 0);
        }

        [Test]
        public async Task AddEventToDb_CorrectAdding()
        {
            // Arrange
            var service = new EventsService(context);

            // Act
            await service.AddEventToDb(Events[1]);

            var @event = await context.Events.FirstOrDefaultAsync();

            // Assert
            Assert.AreEqual("Color Fest 2020", @event.Name);

        }

        [Test]
        public async Task UpdEventToDb_CorrectUpdate()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Events.AddRangeAsync(Events);

            await context.SaveChangesAsync();

            var service = new EventsService(context);
            // Act
            await service.UpdEventToDb(UpdatedEvent);

            var resultEvent = await context.Events.Where(c => c.Name == UpdatedEvent.Name).FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(resultEvent);
        }

        [Test]
        public async Task RemoveEventFromDb_CorrectRemove()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Events.AddRangeAsync(Events);

            await context.SaveChangesAsync();

            var service = new EventsService(context);

            // Act
            await service.RemoveEventFromDb(Events[3]);

            // Assert
            Assert.AreEqual(9, context.Events.Count());
            Assert.IsNull(await context.Events.Where(e => e.Name == "Magic garden. Festival of Music and Theater").FirstOrDefaultAsync());
        }

        [Test]
        public async Task GetEventsCategories_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.EventCategories.AddRangeAsync(EventCategories);
            await context.SaveChangesAsync();

            var service = new EventsService(context);

            // Act
            var eventsCategories = await service.GetEventsCategories();

            // Assert
            Assert.IsNotNull(eventsCategories);
            Assert.IsTrue(eventsCategories.Count > 0);
        }

        [Test]
        public async Task GetEventsCategories_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new EventsService(context);

            // Act
            var eventsCategories = await service.GetEventsCategories();

            // Assert
            Assert.IsEmpty(eventsCategories);
        }

        [Test]
        public async Task GetEventsByCategoryId_CorrectIdProvided_CorrectEventsReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Events.AddRangeAsync(Events);
            await context.EventCategories.AddRangeAsync(EventCategories);
            await context.SaveChangesAsync();

            var service = new EventsService(context);

            // Act
            var events = await service.GetEventsByCategoryId(2);

            // Assert
            Assert.IsNotNull(events);
        }

        [Test]
        public async Task GetEventWithTickets_CorrectEventIdProvided_CorrectResultReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);
            await context.EventCategories.AddRangeAsync(EventCategories);
            await context.Cities.AddRangeAsync(Cities);
            await context.Events.AddRangeAsync(Events);
            await context.Tickets.AddRangeAsync(Tickets);
            await context.Orders.AddRangeAsync(Orders);
            await context.SaveChangesAsync();

            var service = new EventsService(context);

            // Act
            var eventWithTicketsModel = await service.GetEventWithTickets(1);

            // Assert
            Assert.IsNotNull(eventWithTicketsModel);
            Assert.IsNotNull(eventWithTicketsModel.eventTickets);
        }

        [Test]
        public async Task GetEventWithTickets_IncorrectEventIdProvided_EmtyObjectReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);
            await context.EventCategories.AddRangeAsync(EventCategories);
            await context.Cities.AddRangeAsync(Cities);
            await context.Events.AddRangeAsync(Events);
            await context.Tickets.AddRangeAsync(Tickets);
            await context.Orders.AddRangeAsync(Orders);
            await context.SaveChangesAsync();

            var service = new EventsService(context);

            // Act
            var eventWithTicketsModel = await service.GetEventWithTickets(0);

            // Assert
            Assert.IsNotNull(eventWithTicketsModel);
            Assert.IsNull(eventWithTicketsModel.Event);
            Assert.IsNull(eventWithTicketsModel.eventTickets);
        }

        [Test]
        public async Task AddVenueToDb_CorrectAdding()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new VenuesService(context);

            // Act
            await context.AddRangeAsync(Cities);
            await context.SaveChangesAsync();

            await service.AddVenueToDb(Venues[1]);

            var venue = await context.Venues.FirstOrDefaultAsync();

            // Assert
            Assert.AreEqual("Pyshki forest park", venue.Name);

        }

        [Test]
        public async Task UpdVenueToDb_CorrectUpdate()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Venues.AddRangeAsync(Venues);

            await context.SaveChangesAsync();

            var service = new VenuesService(context);
            // Act
            await service.UpdVenueToDb(UpdatedVenue);

            var resultVenue = await context.Venues.Where(c => c.Name == UpdatedVenue.Name).FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(resultVenue);
        }

        [Test]
        public async Task RemoveVenueFromDb_CorrectRemove()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Venues.AddRangeAsync(Venues);

            await context.SaveChangesAsync();

            var service = new VenuesService(context);

            // Act
            await service.RemoveVenueFromDb(Venues[3]);

            // Assert
            Assert.AreEqual(6, context.Cities.Count());
            Assert.IsNull(await context.Cities.Where(c => c.Name == "Central botanical garden").FirstOrDefaultAsync());
        }

        [Test]
        public async Task GetVenues_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Venues.AddRangeAsync(Venues);

            await context.SaveChangesAsync();

            var service = new VenuesService(context);

            // Act
            var venues = await service.GetVenues();

            // Assert
            Assert.IsNotEmpty(venues);
        }

        [Test]
        public async Task GetVenues_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new VenuesService(context);

            // Act
            var venues = await service.GetVenues();

            // Assert
            Assert.IsEmpty(venues);
        }

        [Test]
        public async Task GetVenueById_CorrectCityProvided_CorrecCityReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Venues.AddRangeAsync(Venues);

            await context.SaveChangesAsync();

            var service = new VenuesService(context);

            // Act
            var resultVenue = await service.GetVenueById(3);

            // Assert
            Assert.IsNotNull(resultVenue);
            Assert.AreEqual("Independense sq.", resultVenue.Name);
        }

        [Test]
        public async Task GetVenueById_NullProvided_NullReturned()
        {
            // Arrange
            var service = new VenuesService(context);

            // Act
            var venue = await service.GetVenueById(999);

            // Assert
            Assert.IsNull(venue);

        }

        [Test]
        public async Task GetVenueNameById_CorrectCityProvided_CorrecCityReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Venues.AddRangeAsync(Venues);

            await context.SaveChangesAsync();

            var service = new VenuesService(context);

            // Act
            var venueName = await service.GetVenueNameById(3);

            // Assert
            Assert.AreEqual("Independense sq.", venueName);
        }

        [Test]
        public async Task GetVenueNameById_NullProvided_NullReturned()
        {
            // Arrange
            var service = new VenuesService(context);

            // Act
            var venue = await service.GetVenueNameById(999);

            // Assert
            Assert.IsNull(venue);

        }

        [Test]
        public async Task GetOrders_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Orders.AddRangeAsync(Orders);

            await context.SaveChangesAsync();

            var service = new OrdersService(context);

            // Act
            var orders = await service.GetOrders();

            // Assert
            Assert.IsNotEmpty(orders);
        }

        [Test]
        public async Task GetOrders_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new OrdersService(context);

            // Act
            var orders = await service.GetOrders();

            // Assert
            Assert.IsEmpty(orders);
        }

        [Test]
        public async Task GetOrdersByTicketId_CorrectCityProvided_CorrecCityReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Orders.AddRangeAsync(Orders);

            await context.SaveChangesAsync();

            var service = new OrdersService(context);

            // Act
            var resultOrders = await service.GetOrdersByTicketId(1);

            // Assert
            Assert.IsNotNull(resultOrders);
            Assert.IsTrue(resultOrders.Count > 0);
        }

        [Test]
        public async Task GetOrdersByTicketId_EmptyProvided_EmptyReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new OrdersService(context);

            // Act
            var orders = await service.GetOrdersByTicketId(999);

            // Assert
            Assert.IsEmpty(orders);

        }

        [Test]
        public async Task GetOrdersByUserName_CorrectCityProvided_CorrecCityReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Orders.AddRangeAsync(Orders);

            await context.SaveChangesAsync();

            var service = new OrdersService(context);

            // Act
            var resultOrders = await service.GetOrdersByUserName(Users[0].UserName);

            // Assert
            Assert.IsNotNull(resultOrders);
            Assert.IsTrue(resultOrders.Count > 0);
        }

        [Test]
        public async Task GetOrdersByUserName_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new OrdersService(context);

            // Act
            var orders = await service.GetOrdersByUserName(null);

            // Assert
            Assert.IsNull(orders);

        }

        [Test]
        public async Task GetOrdersByUserName_EmptyStringProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new OrdersService(context);

            // Act
            var orders = await service.GetOrdersByUserName("");

            // Assert
            Assert.IsNull(orders);

        }

        [Test]
        public async Task AddTicketToOrder_CorrectAdding()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.AddRangeAsync(Cities);
            await context.Users.AddRangeAsync(Users);
            await context.Tickets.AddRangeAsync(Tickets);
            await context.SaveChangesAsync();

            var service = new OrdersService(context);

            // Act
            await service.AddTicketToOrder(Users[0].UserName, Tickets[4]);

            var userOrders = await context.Orders.ToListAsync();

            // Assert
            Assert.IsNotNull(userOrders);
            Assert.IsTrue(userOrders.Count() > 0);

        }

        [Test]
        public async Task AddTicketToOrder_IncorrectAdding_UserNullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.AddRangeAsync(Cities);
            await context.Users.AddRangeAsync(Users);
            await context.Tickets.AddRangeAsync(Tickets);
            await context.SaveChangesAsync();

            var service = new OrdersService(context);

            // Act
            await service.AddTicketToOrder(null, Tickets[4]);

            var userOrders = await context.Orders.ToListAsync();

            // Assert
            Assert.IsEmpty(userOrders);
        }

        [Test]
        public async Task UpdOrderToDb_CorrectUpdate()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Orders.AddRangeAsync(Orders);

            await context.SaveChangesAsync();

            var service = new OrdersService(context);
            // Act
            await service.UpdOrderToDb(UpdatedOrder);

            var resultOrder = await context.Orders.Where(o => o.TrackNumber == UpdatedOrder.TrackNumber).FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(resultOrder);
        }

        [Test]
        public async Task GetUsers_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            var users = await service.GetUsers();

            // Assert
            Assert.IsNotEmpty(users);
        }

        [Test]
        public async Task GetUsers_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var users = await service.GetUsers();

            // Assert
            Assert.IsEmpty(users);
        }

        [Test]
        public async Task GetRoles_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Roles.AddRangeAsync(Roles);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            var roles = await service.GetRoles();

            // Assert
            Assert.IsNotEmpty(roles);
        }

        [Test]
        public async Task GetRoles_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var roles = await service.GetRoles();

            // Assert
            Assert.IsEmpty(roles);
        }

        [Test]
        public async Task UpdUserFirstName_CorrectUpdate()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);
            // Act
            await service.UpdUserFirstName(Users[0], "Alexandr");

            var user = await context.Users.Where(u => u.FirstName == "Alexandr").FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(user);
        }

        [Test]
        public async Task UpdUserFirstName_IncorrectUpdate_NullFirstName()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            await service.UpdUserFirstName(Users[0], null);

            var user = await context.Users.Where(u => u.FirstName == "Alexandr").FirstOrDefaultAsync();

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public async Task UpdUserFirstName_IncorrectUpdate_NullUser()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            await service.UpdUserFirstName(null, "Alexandr");

            var user = await context.Users.Where(u => u.FirstName == "Alexandr").FirstOrDefaultAsync();

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public async Task UpdUserLastName_CorrectUpdate()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);
            // Act
            await service.UpdUserLastName(Users[0], "Abramov");

            var user = await context.Users.Where(u => u.LastName == "Abramov").FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(user);
        }

        [Test]
        public async Task UpdUserLastName_IncorrectUpdate_NullLastName()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            await service.UpdUserLastName(Users[0], null);

            var user = await context.Users.Where(u => u.LastName == "Abramov").FirstOrDefaultAsync();

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public async Task UpdUserLastName_IncorrectUpdate_NullUser()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            await service.UpdUserLastName(null, "Abramov");

            var user = await context.Users.Where(u => u.LastName == "Abramov").FirstOrDefaultAsync();

            // Assert
            Assert.IsNull(user);
        }

        public async Task UpdUserAddress_CorrectUpdate()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);
            // Act
            await service.UpdUserAddress(Users[0], "Bangalor sq., 15");

            var user = await context.Users.Where(u => u.Address == "Bangalor sq., 15").FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(user);
        }

        [Test]
        public async Task UpdUserAddress_IncorrectUpdate_NullAddress()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            await service.UpdUserAddress(Users[0], null);

            var user = await context.Users.Where(u => u.Address == "Bangalor sq., 15").FirstOrDefaultAsync();

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public async Task UpdUserAddress_IncorrectUpdate_NullUser()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            await service.UpdUserAddress(null, "Bangalor sq., 15");

            var user = await context.Users.Where(u => u.Address == "Bangalor sq., 15").FirstOrDefaultAsync();

            // Assert
            Assert.IsNull(user);
        }

        public async Task UpdUserLocalization_CorrectUpdate()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);
            // Act
            await service.UpdUserLocalization(Users[0], Localizations.eng);

            var user = await context.Users.Where(u => u.Localization ==  Localizations.eng).FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(user);
        }

        [Test]
        public async Task UpdUserLocalization_IncorrectUpdate_IncorrectLocalization()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            await service.UpdUserLocalization(Users[0], new Localizations { });

            var user = await context.Users.Where(u => u.Localization == new Localizations { }).FirstOrDefaultAsync();

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public async Task GetUserFirstNameByUserName_CorrectUserNameProvided_CorrectFirstNameReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            var userFirstName = await service.GetUserFirstNameByUserName(Users[0].UserName);

            // Assert
            Assert.IsNotNull(userFirstName);
            Assert.IsTrue(userFirstName == Users[0].FirstName);
        }

        [Test]
        public async Task GetUserFirstNameByUserName_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var userFirstName = await service.GetUserFirstNameByUserName(null);

            // Assert
            Assert.IsNull(userFirstName);

        }

        [Test]
        public async Task GetUserFirstNameByUserName_EmptyStringProvided_NullReturned()
        {
            // Arrange
            var service = new UsersAndRolesService(context);

            // Act
            var userFirstName = await service.GetUserFirstNameByUserName("");

            // Assert
            Assert.IsNull(userFirstName);

        }


        [Test]
        public async Task GetUserLastNameByUserName_CorrectUserNameProvided_CorrectLastNameReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            var userLastName = await service.GetUserLastNameByUserName(Users[0].UserName);

            // Assert
            Assert.IsNotNull(userLastName);
            Assert.IsTrue(userLastName == Users[0].LastName);
        }

        [Test]
        public async Task GetUserLastNameByUserName_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var userLastName = await service.GetUserLastNameByUserName(null);

            // Assert
            Assert.IsNull(userLastName);

        }

        [Test]
        public async Task GetUserLastNameByUserName_EmptyStringProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var userLastName = await service.GetUserLastNameByUserName("");

            // Assert
            Assert.IsNull(userLastName);

        }

        [Test]
        public async Task GetUserAddressByUserName_CorrectUserNameProvided_CorrectAddressReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            var userAddress = await service.GetUserAddressByUserName(Users[0].UserName);

            // Assert
            Assert.IsNotNull(userAddress);
            Assert.IsTrue(userAddress == Users[0].Address);
        }

        [Test]
        public async Task GetUserAddressByUserName_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var userAddress = await service.GetUserAddressByUserName(null);

            // Assert
            Assert.IsNull(userAddress);

        }

        [Test]
        public async Task GetUserAddressByUserName_EmptyStringProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var userAddress = await service.GetUserAddressByUserName("");

            // Assert
            Assert.IsNull(userAddress);

        }

        [Test]
        public async Task GetUserLocalizationByUserName_CorrectUserNameProvided_CorrectLocalizationReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            var userLocalization = await service.GetUserLocalizationByUserName(Users[0].UserName);

            // Assert
            Assert.IsNotNull(userLocalization);
            Assert.IsTrue(userLocalization == Users[0].Localization);
        }

        [Test]
        public async Task GetUserLocalizationByUserName_NullProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var userLocalization = await service.GetUserLocalizationByUserName(null);

            // Assert
            Assert.IsTrue(userLocalization == 0);

        }

        [Test]
        public async Task GetUserLocalizationByUserName_EmptyStringProvided_NullReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var userLocalization = await service.GetUserLocalizationByUserName("");

            // Assert
            Assert.IsTrue(userLocalization == 0);

        }


        [Test]
        public async Task AddTicketToDb_CorrectAdding()
        {
            // Arrange
            var service = new TicketsService(context);

            // Act
            await service.AddTicketToDb(Tickets[1]);

            var tickets = await context.Tickets.ToListAsync();

            // Assert
            Assert.IsNotNull(tickets);
            Assert.IsTrue(Tickets.Count() > 0);
        }

        [Test]
        public async Task UpdTicketToDb_CorrectUpdate()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Tickets.AddRangeAsync(Tickets);

            await context.SaveChangesAsync();

            var service = new TicketsService(context);
            // Act
            await service.UpdTicketToDb(UpdatedTicket);

            var resultTicket = await context.Tickets.Where(t => t.Price == UpdatedTicket.Price).FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(resultTicket);
        }

        [Test]
        public async Task GetTicketsByStatusAndUserName_CorrectDataProvided_CorrecResultReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Tickets.AddRangeAsync(Tickets);

            await context.SaveChangesAsync();

            var service = new TicketsService(context);

            // Act
            var resultTickets = await service.GetTicketsByStatusAndUserName(TicketStatuses.waiting, Users[0].UserName);

            // Assert
            Assert.IsNotNull(resultTickets);
            Assert.IsTrue(resultTickets.Count > 0);
        }

        [Test]
        public async Task GetTicketsByStatusAndUserName_NullProvided_EmptyReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new TicketsService(context);

            // Act
            var tickets = await service.GetTicketsByStatusAndUserName(TicketStatuses.waiting, null);

            // Assert
            Assert.IsEmpty(tickets);

        }

        [Test]
        public async Task GetTicketsByStatusAndUserName_EmptyStringProvided_EmptyReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new TicketsService(context);

            // Act
            var tickets = await service.GetTicketsByStatusAndUserName(TicketStatuses.waiting, "");

            // Assert
            Assert.IsEmpty(tickets);

        }

        [Test]
        public async Task GetTicketById_CorrectCityProvided_CorrecCityReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Tickets.AddRangeAsync(Tickets);

            await context.SaveChangesAsync();

            var service = new TicketsService(context);

            // Act
            var resultTicket = await service.GetTicketById(3);

            // Assert
            Assert.IsNotNull(resultTicket);
            Assert.AreEqual(130, resultTicket.Price);
        }

        [Test]
        public async Task GetTicketById_NullProvided_NullReturned()
        {
            // Arrange
            var service = new TicketsService(context);

            // Act
            var ticket = await service.GetTicketById(999);

            // Assert
            Assert.IsNull(ticket);

        }

        [Test]
        public async Task GetTicketsByUserId_CorrectIdProvided_CorrecTicketsReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Tickets.AddRangeAsync(Tickets);
            await context.Orders.AddRangeAsync(Orders);

            await context.SaveChangesAsync();

            var service = new TicketsService(context);

            // Act
            var resultTickets = await service.GetTicketsByUserId(Users[0].Id);

            // Assert
            Assert.IsNotNull(resultTickets);
            Assert.IsTrue(resultTickets.Count > 0);
        }

        [Test]
        public async Task GetTicketsByUserId_NullProvided_NullReturned()
        {
            // Arrange
            var service = new TicketsService(context);

            // Act
            var tickets = await service.GetTicketsByUserId(null);

            // Assert
            Assert.IsEmpty(tickets);
            Assert.IsTrue(tickets.Count == 0);

        }

        [Test]
        public async Task GetUsersRolesByUsers_NullProvided_EmptyReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var usersRoles = await service.GetUsersRolesByUsers(null);

            // Assert
            Assert.IsEmpty(usersRoles);

        }

        [Test]
        public async Task GetUsersRolesByUsers_CorretDataProvided_CorrectResultReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);
            await context.Roles.AddRangeAsync(Roles);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            var usersRoles = await service.GetUsersRolesByUsers(new List<StoreUser> { Users[0], Users[1]});

            // Assert
            Assert.IsNotNull(usersRoles);

        }

        [Test]
        public async Task GetRolesByUsersRoles_NullProvided_EmptyReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            var service = new UsersAndRolesService(context);

            // Act
            var roles = await service.GetRolesByUsersRoles(null);

            // Assert
            Assert.IsEmpty(roles);

        }

        [Test]
        public async Task GetRolesByUsersRoles_CorretDataProvided_CorrectResultReturned()
        {
            // Arrange
            context = factory.CreateContextForSQLite();

            await context.Users.AddRangeAsync(Users);
            await context.Roles.AddRangeAsync(Roles);

            await context.SaveChangesAsync();

            var service = new UsersAndRolesService(context);

            // Act
            var roles = await service.GetRolesByUsersRoles(UsersRoles);

            // Assert
            Assert.IsNotNull(roles);
            Assert.AreEqual(2, roles.Count);
        }
    }
}


    



    

