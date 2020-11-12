using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NUnit.Framework;
using TicketsResale.Business.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TicketsResale.Test
{
    public class SQLiteModelsTest
    {

        [Test]
        public async Task AddUser()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            var user = new StoreUser { FirstName = "Alexey", LastName = "Karm", Address = "15, Kosmonavtov Av., Grodno, BLR", Localization = Localizations.rus };
            
            // Act    
            var data = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            // Assert   
            Assert.IsNotEmpty(await context.Users.ToListAsync());
        }

        [Test]
        public async Task AddRole()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            var role = new IdentityRole { Name = "Administrator" };

            // Act    
            var data = await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();

            // Assert   
            Assert.IsNotEmpty(context.Roles.ToList());
        }

        [Test]
        public async Task AddEventCategory()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            var eventCategory = new EventCategory { Name = "Sports" };

            // Act    
            var data = await context.EventCategories.AddAsync(eventCategory);
            await context.SaveChangesAsync();

            // Assert   
            Assert.IsNotEmpty(context.EventCategories.ToList());
        }


        [Test]
        public async Task AddUsers_Time_Test()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            // Act   
            for (int i = 1; i <= 1000; i++)
            {
                var user = new StoreUser { FirstName = "Alexey", LastName = "Karm", Address = "15, Kosmonavtov Av., Grodno, BLR", Localization = Localizations.rus, PhoneNumber = "228228", UserName = "user" + i + "@mail.ru", Email = "alexey.karm@mail.ru" };
                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();

            // Assert    
            var usersCount = context.Users.Count();
            if (usersCount != 0)
            {
                Assert.AreEqual(1000, usersCount);
            }

            var singleUser = await context.Users.FirstOrDefaultAsync();
            if (singleUser != null)
            {
                Assert.AreEqual("Alexey", singleUser.FirstName);
            }
        }

        [Test]
        public async Task AddCity()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            var city = new City { Name = "Grodno" };

            // Act    
            var data = await context.Cities.AddAsync(city);
            await context.SaveChangesAsync();

            // Assert   
            Assert.IsNotEmpty(context.Cities.ToList());
        }

        [Test]
        public async Task AddCities_Time_Test()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            // Act   
            for (int i = 1; i <= 1000; i++)
            {
                var city = new City { Name = "City " + i };
                await context.Cities.AddAsync(city);
            }

            await context.SaveChangesAsync();

            // Assert    
            var citiesCount = context.Cities.Count();
            if (citiesCount != 0)
            {
                Assert.AreEqual(1000, citiesCount);
            }

            var singleCity = await context.Cities.FirstOrDefaultAsync();
            if (singleCity != null)
            {
                Assert.AreEqual("City 1", singleCity.Name);
            }
        }

        //============================================================

        [Test]
        public async Task AddVenue_Without_Relation()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            var venue = new Venue() { Name = "Sovetskaya sq.", Address = "Sovetskaya sq." };

            // Act    
            var data = await context.Venues.AddAsync(venue);

            // Assert   
            Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            Assert.IsEmpty(context.Venues.ToList());
        }

        [Test]
        public async Task AddVenue_With_Relation_Return_Exception()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            var venue = new Venue { Name = "Sovetskaya sq.", Address = "Sovetskaya sq.", CityId = 1 };

            // Act    
            var data = await context.Venues.AddAsync(venue);

            // Assert   
            Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            Assert.IsEmpty(context.Venues.ToList());
        }

        [Test]
        public async Task AddVenue_With_Relation_Return_No_Exception()
        {
            // Arrange    
            var factory = new ConnectionFactory();
 
            var context = factory.CreateContextForSQLite();
            var venue = new Venue { Name = "Sovetskaya sq.", Address = "Sovetskaya sq.", CityId = 1};

            // Act    
            for (int i = 1; i < 4; i++)
            {
                var city = new City() { Id = i, Name = "City " + i };
                await context.Cities.AddAsync(city);
            }
            await context.SaveChangesAsync();

            var data = await context.Venues.AddAsync(venue);
            await context.SaveChangesAsync();

            // Assert             
            var venuesCount = context.Venues.Count();
            if (venuesCount != 0)
            {
                Assert.AreEqual(1, venuesCount);
            }

            var singleVenue = await context.Venues.FirstOrDefaultAsync();
            if (singleVenue != null)
            {
                Assert.AreEqual("Sovetskaya sq.", singleVenue.Name);
            }
        }

        [Test]
        public async Task AddVenues_Time_Test()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            // Act   
            for (int i = 1; i < 4; i++)
            {
                var city = new City() { Id = i, Name = "City " + i };
                await context.Cities.AddAsync(city);
            }
            await context.SaveChangesAsync();

            for (int i = 1; i <= 1000; i++)
            {
                var venue = new Venue { Name = "Venue name " + i, Address = "Some venue address " + i, CityId = 1 };
                await context.Venues.AddAsync(venue);
            }

            await context.SaveChangesAsync();

            // Assert    
            var venuesCount = context.Venues.Count();
            if (venuesCount != 0)
            {
                Assert.AreEqual(1000, venuesCount);
            }

            var singleVenue = await context.Venues.FirstOrDefaultAsync();
            if (singleVenue != null)
            {
                Assert.AreEqual(1, singleVenue.Id);
            }
        }

        //============================================================

        [Test]
        public async Task AddEvent_Without_Relation()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            var @event = new Event { Name = "Festival of national cultures 2021", Date = new DateTime(2021, 06, 05), Banner = "events/fnk-grodno.jpg", Description = "The <strong>Republican Festival of National Cultures</strong> is a holiday of folklore colors of various peoples living in <em>Belarus</em>. Representatives of <strong>36</strong> nationalities participate in the festival, the attendance of the festival in <strong>2018</strong> was <strong>270</strong> thousand people." };

            // Act    
            var data = await context.Events.AddAsync(@event);

            // Assert   
            Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            Assert.IsEmpty(context.Events.ToList());
        }

        [Test]
        public async Task AddEvent_With_Relation_Return_Exception()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            var @event = new Event { Name = "Festival of national cultures 2021", EventCategoryId = 1, VenueId  = 1, Date = new DateTime(2021, 06, 05), Banner = "events/fnk-grodno.jpg", Description = "The <strong>Republican Festival of National Cultures</strong> is a holiday of folklore colors of various peoples living in <em>Belarus</em>. Representatives of <strong>36</strong> nationalities participate in the festival, the attendance of the festival in <strong>2018</strong> was <strong>270</strong> thousand people." };

            // Act    
            var data = await context.Events.AddAsync(@event);

            // Assert   
            Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            Assert.IsEmpty(context.Events.ToList());
        }

        [Test]
        public async Task AddEvent_With_Relation_Return_No_Exception()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();
 
            // Act    
            for (int i = 1; i < 4; i++)
            {
                var category = new EventCategory() { Id = i, Name = "EventCategory " + i };
                await context.EventCategories.AddAsync(category);
            }
            await context.SaveChangesAsync();

            await context.Cities.AddAsync(new City {Id = 1, Name = "Lviv" });
            await context.SaveChangesAsync();

            for (int i = 1; i < 4; i++)
            {
                var venueObj = new Venue { Name = "Venue " + i, Address = "Sovetskaya sq.", CityId = 1 };
                await context.Venues.AddAsync(venueObj);
            }
            await context.SaveChangesAsync();

            var @event = new Event { Name = "Festival of national cultures 2021", EventCategoryId = 1, VenueId = 2, Date = new DateTime(2021, 06, 05), Banner = "events/fnk-grodno.jpg", Description = "The <strong>Republican Festival of National Cultures</strong> is a holiday of folklore colors of various peoples living in <em>Belarus</em>. Representatives of <strong>36</strong> nationalities participate in the festival, the attendance of the festival in <strong>2018</strong> was <strong>270</strong> thousand people." };


            var data = await context.Events.AddAsync(@event);
            await context.SaveChangesAsync();

            // Assert             
            var eventsCount = context.Events.Count();
            if (eventsCount != 0)
            {
                Assert.AreEqual(1, eventsCount);
            }

            var singleEvent = await context.Events.FirstOrDefaultAsync();
            if (singleEvent != null)
            {
                Assert.AreEqual("Festival of national cultures 2021", singleEvent.Name);
            }
        }

        [Test]
        public async Task AddEvents_Time_Test()
        {
            // Arrange    
            var factory = new ConnectionFactory();

            var context = factory.CreateContextForSQLite();

            // Act   
            for (int i = 1; i < 4; i++)
            {
                var category = new EventCategory() { Id = i, Name = "EventCategory " + i };
                await context.EventCategories.AddAsync(category);
            }
            await context.SaveChangesAsync();

            await context.Cities.AddAsync(new City { Id = 1, Name = "Lviv" });
            await context.SaveChangesAsync();

            for (int i = 1; i < 4; i++)
            {
                var venueObj = new Venue { Name = "Venue " + i, Address = "Sovetskaya sq.", CityId = 1 };
                await context.Venues.AddAsync(venueObj);
            }
            await context.SaveChangesAsync();

            for (int i = 1; i <= 1000; i++)
            {
                var @event = new Event() { Id = i, Name = "Event " + i, VenueId = 1, EventCategoryId = 1 };
                await context.Events.AddAsync(@event);
            }

            await context.SaveChangesAsync();

            // Assert    
            var eventsCount = context.Events.Count();
            if (eventsCount != 0)
            {
                Assert.AreEqual(1000, eventsCount);
            }

            var singleEvent = await context.Events.FirstOrDefaultAsync();
            if (singleEvent != null)
            {
                Assert.AreEqual(1, singleEvent.Id);
            }
        }

        //============================================================


    }
}
