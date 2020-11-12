using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NUnit.Framework;
using TicketsResale.Business.Models;
using Microsoft.AspNetCore.Identity;
using TicketsResale.Models.Service;
using TicketsResale.Models;
using System.Threading.Tasks;

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

        [OneTimeSetUp]
        public void Setup()
        {
            Cities = new List<City>()
            {
                new City { Name = "Grodno" },
                new City { Name = "Minsk" },
                new City { Name = "Barselona" },
                new City { Name = "NewYork" },
                new City { Name = "Tokyo" },
                new City { Name = "Dubai" }
            };

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
        }

        [Test]
        public async Task GetCities_CorrectListProvided_CorrectTotalReturned()
        {
            // Arrange
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            foreach (City city in Cities)
            { 
                context.Cities.Add(city); 
            }
            context.SaveChanges();

            var service = new CitiesService(context);

            // Act
            var cities = await service.GetCities();

            // Assert
            Assert.IsNotNull(cities);
        }

        [Test]
        public async Task GetCities_NullProvided_NullReturned()
        {
            // Arrange
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            var service = new CitiesService(context);

            // Act
            var cities = await service.GetCities();

            // Assert
            Assert.IsEmpty(cities);
        }

        [Test]
        public async Task GetCityById_CorrectCityProvided_CorrecCityReturned()
        {
            // Arrange
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            foreach (City city in Cities)
            {
                context.Cities.Add(city);
            }
            context.SaveChanges();

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
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

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
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            foreach (City city in Cities)
            {
                context.Cities.Add(city);
            }
            context.SaveChanges();

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
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

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
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

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
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            foreach (City city in Cities)
            {
                await context.Cities.AddAsync(city);
            }
            await context.SaveChangesAsync();

            var service = new CitiesService(context);

            Cities[1].Name = "Kamenets";

            // Act
            await service.UpdCityToDb(Cities[1]);

            var resultCity = await context.Cities.Where(c => c.Name == Cities[1].Name).FirstOrDefaultAsync();

            // Assert
            Assert.IsNotNull(resultCity);
        }

        [Test]
        public async Task RemoveCityFromDb_CorrectRemove()
        {
            // Arrange
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            foreach (City city in Cities)
            {
                await context.Cities.AddAsync(city);
            }
            await context.SaveChangesAsync();

            var service = new CitiesService(context);


            // Act
            await service.RemoveCityFromDb(Cities[3]);

            // Assert
            Assert.AreEqual(5, context.Cities.Count());
            Assert.IsNull(await context.Cities.Where(c => c.Name == "NewYork").FirstOrDefaultAsync());
        }

    }
}


    



    

