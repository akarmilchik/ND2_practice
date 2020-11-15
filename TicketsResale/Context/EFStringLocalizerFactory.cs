using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Context
{
    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        string _connectionString;
        public EFStringLocalizerFactory(string connection)
        {
            _connectionString = connection;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return CreateStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return CreateStringLocalizer();
        }

        private IStringLocalizer CreateStringLocalizer()
        {
            StoreContext _db = new StoreContext(
                new DbContextOptionsBuilder<StoreContext>()
                    .UseSqlServer(_connectionString)
                    .Options);
            // инициализация базы данных
            if (!_db.Cultures.Any())
            {
                _db.AddRange(
                    new Culture
                    {
                        Name = "en-US",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Header", Value = "Hello" },
                            new Resource { Key = "Message", Value = "Welcome" },
                            new Resource { Key = "Events", Value = "Events" },
                            new Resource { Key = "Categories", Value = "Categories" },
                            new Resource { Key = "You come", Value = "You have come to the application for the online sale and resale of tickets for events. Enjoy." }
                        }
                    },
                    new Culture
                    {
                        Name = "ru-RU",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Header", Value = "Привет" },
                            new Resource { Key = "Message", Value = "Добро пожаловать" },
                            new Resource { Key = "Events", Value = "События 2" },
                            new Resource { Key = "Categories", Value = "Категории" },
                            new Resource { Key = "You come", Value = "Вы перешли в приложение для онлайн-продажи и перепродажи билетов на мероприятия. Наслаждайтесь." }
                        }
                    },
                    new Culture
                    {
                        Name = "be-BY",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Header", Value = "Прывітанне" },
                            new Resource { Key = "Message", Value = "Сардэчна запрашаем" },
                            new Resource { Key = "Events", Value = "Падзеі" },
                            new Resource { Key = "Categories", Value = "Катэгорыі" },
                            new Resource { Key = "You come", Value = "Вы патрапілі ў дадатак для онлайн-продажу і перапродажу білетаў на мерапрыемствы. Прыемнага карыстання." }
                        }
                    }
                );
                _db.SaveChanges();
            }
            return new EFStringLocalizer(_db);
        }
    }
}
