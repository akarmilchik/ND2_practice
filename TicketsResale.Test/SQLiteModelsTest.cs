using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NUnit.Framework;
using TicketsResale.Business.Models;
using System.Threading.Tasks;

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

        //============================================================

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

    }
}
