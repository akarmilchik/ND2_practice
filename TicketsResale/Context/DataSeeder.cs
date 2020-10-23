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
            new User { Id = 1, FirstName = "Alexey", LastName = "Robinson", Address = "15, Kosmonavtov Av., Grodno, BLR", Localization = "rus", PhoneNumber = "228228", UserName = "admin", Password = "admin", Role = "Administrator" },
            new User { Id = 2, FirstName = "Jominez", LastName = "Maxwell", Address = "132/1, Sunlight Av., Barselona, SPA", Localization = "spa", PhoneNumber = "345124", UserName = "user", Password = "user", Role = "User" },
            new User { Id = 3, FirstName = "Alibaba", LastName = "Bestseller", Address = "6/1, 123 Av., New York, USA", Localization = "eng", PhoneNumber = "777777", UserName = "seller", Password = "seller", Role = "User" }
        };


        private static readonly List<Venue> Venues = new List<Venue>
        {
            new Venue { Id = 1, Name = "Sovetskaya sq.", Address = "Sovetskaya sq.", City = Cities[0] },
            new Venue { Id = 2, Name = "Pyshki forest park", Address = "10, Festivalnaya st.", City = Cities[0] },
            new Venue { Id = 3, Name = "Independense sq.", Address = "15, Independense av.", City = Cities[1] },
            new Venue { Id = 4, Name = "Central botanical garden", Address = "2b, Surganova st.", City = Cities[1] },
            new Venue { Id = 5, Name = "Park Forum", Address = "Carrer de La Pau, 12, Sant Adria De Besos", City = Cities[2] },
            new Venue { Id = 6, Name = "Igulada", Address = "Igulada city", City = Cities[2] },
            new Venue { Id = 7, Name = "Broadway Majestic Theatre", Address = "245 West, 44th Street", City = Cities[3] },
            new Venue { Id = 8, Name = "Radio city Rockettes", Address = "1260 av. btw 50th and 51st st.", City = Cities[3] },
            new Venue { Id = 9, Name = "Fukagawa Sakura", Address = "2-1-8 Monzennakacho, Koto 135-0048 Perfecture", City = Cities[4] },
            new Venue { Id = 10, Name = "Sanja Matsuri", Address = "2-3-1 Asakusa Shrine, Asakusa, Taito 111-0032 Perfecture", City = Cities[4] },
            new Venue { Id = 11, Name = "Dubai world trade center", Address = "Trade Centre 2", City = Cities[5] },
            new Venue { Id = 12, Name = "Global village", Address = "Global village", City = Cities[5] }
        };

        private static readonly List<Event> Events = new List<Event>
        {
            new Event { Id = 1, Name = "Festival of national cultures 2021", Venue = Venues[0], Date = new DateTime(2021, 06, 05), Banner = "events/fnk-grodno.jpg", Description = "The <strong>Republican Festival of National Cultures</strong> is a holiday of folklore colors of various peoples living in <em>Belarus</em>. Representatives of <strong>36</strong> nationalities participate in the festival, the attendance of the festival in <strong>2018</strong> was <strong>270</strong> thousand people." },
            new Event { Id = 2, Name = "Color Fest 2020", Venue = Venues[1], Date = new DateTime(2020, 09, 22), Banner = "events/colorfest-grodno.jpg", Description = "Favorite dance music, tons of natural bright colors and fun contests are just a part of what awaits the guests of the  <strong>Festival of Colors</strong>. An atmosphere of happiness and carelessness will reign here, thousands of smiles of bright faces and warm embraces of colorful merry fellows will create an indescribable feeling of unity that will overwhelm your head!" },
            new Event { Id = 3, Name = "Primavera Sound Barselona", Venue = Venues[4], Date = new DateTime(2021, 06, 14), Banner = "events/primavera-barsa.jpg", Description = "The <strong>Primavera Sound Festival</strong> program <em>(Spanish + English \"Sounds of Spring\")</em> in Barcelona usually shines with the most famous bands and musicians from <ins>around the world.</ins> The event takes place at the end of May or at the end of June and brings together about <strong>100,000</strong> music lovers" },
            new Event { Id = 4, Name = "Magic garden. Festival of Music and Theater", Venue = Venues[3], Date = new DateTime(2020, 09, 14), Banner = "events/botanic-minsk.jpg", Description = "Travel back in time and find yourself on a weekend in ... <strong>Ancient Greece</strong>? Easy! On <ins>September 14-15</ins>, the <strong>Central Botanical Garden</strong> will turn into a small Hellas. A real immersion in the land of ancient gods, the cradle of sciences, culture and art awaits you." },
            new Event { Id = 5, Name = "BALLOON FESTIVAL", Venue = Venues[5], Date = new DateTime(2021, 07, 11), Banner = "events/baloon-igulada.jpg", Description = "From 11 to 14 July, the city of <strong>Igualada</strong>, an hour's drive from Barcelona, will host a hot air balloon festival. Flying in a hot air balloon is romantic and beautiful, and when there are about twenty bright balloons next to you in the sky, it is mesmerizing. In addition, the Igualada festival is considered the largest in <em>Europe</em>." },
            new Event { Id = 6, Name = "The Phantom of the Opera", Venue = Venues[6], Date = new DateTime(2020, 09, 25), Banner = "events/majestic-newyork.jpg", Description = "Based on the <mark>1910</mark> horror novel by <strong>Gaston Leroux</strong>, which has been adapted into countless films, <strong>The Phantom of the Opera</strong> follows a deformed composer who haunts the grand Paris Opera House. Sheltered from the outside world in an underground cavern, the lonely, romantic man tutors and composes operas for <em>Christine</em>, a gorgeous young soprano star-to-be. As <em>Christine’s</em> star rises, and a handsome suitor from her past enters the picture, the Phantom grows mad, terrorizing the opera house owners and company with his murderous ways. Still, <em>Christine</em> finds herself drawn to the mystery man." },
            new Event { Id = 7, Name = "ROCKETTES CHRISTMAS SPECTACULAR 2021", Venue = Venues[7], Date = new DateTime(2020, 12, 24), Banner = "events/rockettes-newyork.jpg", Description = "Few things are as emblematic of <strong>New York City</strong> or the holiday season as <strong>The Rockettes</strong>, a precision dance company that’s been performing at <em>Radio City Music Hall</em> in Manhattan since <mark>1932</mark>. Best known for their eye-high leg-kicking routine, in which dozens of dancers seem to move as one in perfect unison — as well as their delightful annual holiday event <strong>The Radio City Christmas Spectacular</strong> — The Rockettes’ mixture of modern dance and classic ballet has been enjoyed by millions upon millions of people, thanks in part to the troupe’s touring company that brings the fun to theaters all across the country." },
            new Event { Id = 8, Name = "Edo Fukagawa Sakura Festival", Venue = Venues[8], Date = new DateTime(2021, 04, 05), Banner = "events/fukagawa-sakura.png", Description = "<strong>Fukagawa’s annual sakura matsuri</strong> takes place along the <em>Ooyokogawa</em> river, with a seemingly neverending trail of cherry blossoms in full bloom. Out of all the sakura festivals <strong>Tokyo</strong> has to offer, this is the only place you can admire the beautiful scenery from below while cruising in Japanese-style boats. A pair of shamisen players will also be travelling onboard while performing ‘Shinnai-nagashi’, a traditional Japanese tune that will transport you back to the Edo period (1603-1868)." },
            new Event { Id = 9, Name = "Sanja Matsuri Festival", Venue = Venues[9], Date = new DateTime(2020, 10, 17), Banner = "events/sanja-matsuri.jpg", Description = "<strong>The Sanja Festival</strong> (<em>三社祭, Sanja Matsuri</em>) is an annual festival in the <strong>Asakusa</strong> district that usually takes place over the third full weekend in May. It is held in celebration of the three founders of Sensoji Temple, who are enshrined in Asakusa Shrine next door to the temple. Nearly two million people visit Asakusa over the three days of the festival, making it one of Tokyo's most popular festivals." },
            new Event { Id = 10, Name = "Global village 25th season", Venue = Venues[11], Date = new DateTime(2021, 04, 04), Banner = "events/global-village.jpg", Description = "<strong>The World Village Dubai</strong> will feature a total of <mark>78</mark> countries. With, new additions- Azerbaijan and Korea, the place promises to be too much fun! The countries will have 26 pavilions that will have cuisines, shows and products from all around the globe." }
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
/*
            if (!context.Users.Any())
            {
                await context.Users.AddRangeAsync(Users);
            }
*/
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
