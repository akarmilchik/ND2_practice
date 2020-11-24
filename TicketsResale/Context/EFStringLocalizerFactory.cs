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
                            new Resource { Key = "You come", Value = "You have come to the application for the online sale and resale of tickets for events. Enjoy." },
                            new Resource { Key = "homepagetitle", Value = "Home page" },
                            new Resource { Key = "Privacy", Value = "Privacy" },
                            new Resource { Key = "eventstitle", Value = "Events" },
                            new Resource { Key = "ordersTitle", Value = "Orders" },
                            new Resource { Key = "ticketsTitle", Value = "Tickets" },
                            new Resource { Key = "myTicketsTitle", Value = "My tickets" },
                            new Resource { Key = "createTicket", Value = "Create ticket" },
                            new Resource { Key = "login", Value = "Log in" },
                            new Resource { Key = "Forgot your password?", Value = "Forgot your password?" },
                            new Resource { Key = "Register as a new user", Value = "Register as a new user" },
                            new Resource { Key = "Use another service to log in.", Value = "Use another service to log in." },
                            new Resource { Key = "externalAuth", Value = "There are no external authentication services configured. See " },
                            new Resource { Key = "this article", Value = "this article" },
                            new Resource { Key = "detailsASP", Value = "for details on setting up this ASP.NET application to support logging in via external services." },
                            new Resource { Key = "Log in using your", Value = "Log in using your" },
                            new Resource { Key = "account", Value = "account" },
                            new Resource { Key = "Welcome", Value = "Welcome" },
                            new Resource { Key = "Cities", Value = "Cities" },
                            new Resource { Key = "Events", Value = "Events" },
                            new Resource { Key = "Venues", Value = "Venues" },
                            new Resource { Key = "Users", Value = "Users" },
                            new Resource { Key = "Categories", Value = "Categories" },
                            new Resource { Key = "Submit", Value = "Submit" },
                            new Resource { Key = "Venue", Value = "Venue" },
                            new Resource { Key = "Buy ticket", Value = "Buy ticket" },
                            new Resource { Key = "BY", Value = "BY" },
                            new Resource { Key = "EN", Value = "EN" },
                            new Resource { Key = "RU", Value = "RU" },
                            new Resource { Key = "Privacy policy", Value = "Privacy policy" },
                            new Resource { Key = "txt1", Value = "This privacy policy has been compiled to better serve those who are concerned with how their \"Personally identifiable information\" (PII) is being used online. PI is information that can be used on its own or with other information to identify, contact, or locate a single person, or to identify an individual in context. Please read our privacy policy carefully to get a clear understanding of how we collect, use, protect or otherwise handle your Personally Identifiable Information in accordance with our website." },
                            new Resource { Key = "txt2", Value = "Our Commitment to User Privacy:" },
                            new Resource { Key = "txt3", Value = "This Policy describes how Privacy.com protects and manages your personal data, including:" },
                            new Resource { Key = "txt4", Value = "What data we collect when you sign-up for, use, or engage with any of our applications, products, services or websites (collectively \"Services\");" },
                            new Resource { Key = "txt5", Value = "How we manage data from the beginning of your engagement forward;" },
                            new Resource { Key = "txt6", Value = "The choices you have regarding how data is managed." },
                            new Resource { Key = "txt7", Value = "We Limit Use of Your Personal Data:" },
                            new Resource { Key = "txt8", Value = "To providing, maintaining and improving our Services;" },
                            new Resource { Key = "txt9", Value = "To communicating with you about new and existing Services;" },
                            new Resource { Key = "txt10", Value = "To protect the legal rights, property and safety of our Services and users." },
                            new Resource { Key = "Hello", Value = "Hello" },
                            new Resource { Key = "Login", Value = "Login" },
                            new Resource { Key = "Logout", Value = "Logout" },
                            new Resource { Key = "My orders", Value = "My orders" },
                            new Resource { Key = "My tickets", Value = "My tickets" },
                            new Resource { Key = "User info", Value = "User info" },
                            new Resource { Key = "Register", Value = "Register" },
                            new Resource { Key = "Admin panel", Value = "Admin panel" },
                            new Resource { Key = "Home", Value = "Home" },
                            new Resource { Key = "Edit", Value = "Edit" },
                            new Resource { Key = "Delete", Value = "Delete" },
                            new Resource { Key = "Create City", Value = "Create city" },
                            new Resource { Key = "Type here city name", Value = "Type here city name" },
                            new Resource { Key = "Create", Value = "Create" },
                            new Resource { Key = "Back", Value = "Back" },
                            new Resource { Key = "Create event", Value = "Create event" },
                            new Resource { Key = "Type here event description", Value = "Type here event description" },
                            new Resource { Key = "New role", Value = "New role" },
                            new Resource { Key = "Add", Value = "Add" },
                            new Resource { Key = "Use a local account to log in.", Value = "Use a local account to log in." },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },

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
                            new Resource { Key = "You come", Value = "Вы перешли в приложение для онлайн-продажи и перепродажи билетов на мероприятия. Наслаждайтесь." },
                            new Resource { Key = "homepagetitle", Value = "Домашняя страница" },
                            new Resource { Key = "Privacy", Value = "Политика конфиденциальности" },
                            new Resource { Key = "eventstitle", Value = "Мероприятия" },
                            new Resource { Key = "ordersTitle", Value = "Заказы" },
                            new Resource { Key = "ticketsTitle", Value = "Билеты" },
                            new Resource { Key = "myTicketsTitle", Value = "Мои билеты" },
                            new Resource { Key = "createTicket", Value = "Создать билет" },
                            new Resource { Key = "login", Value = "Авторизоваться" },
                            new Resource { Key = "Forgot your password?", Value = "Забыли свой пароль?" },
                            new Resource { Key = "Register as a new user", Value = "Зарегистрируйтесь как новый пользователь" },
                            new Resource { Key = "Use another service to log in.", Value = "Используйте другой сервис для входа в систему." },
                            new Resource { Key = "externalAuth", Value = "Не настроены внешние службы аутентификации. Посмотрите " },
                            new Resource { Key = "this article", Value = "эту статью" },
                            new Resource { Key = "detailsASP", Value = "для получения дополнительных сведений о настройке этого приложения ASP.NET для поддержки входа в систему через внешние службы." },
                            new Resource { Key = "Log in using your", Value = "Войдите, используя свой" },
                            new Resource { Key = "account", Value = "аккаунт" },
                            new Resource { Key = "Welcome", Value = "Добро пожаловать" },
                            new Resource { Key = "Cities", Value = "Города" },
                            new Resource { Key = "Events", Value = "События" },
                            new Resource { Key = "Venues", Value = "Площадки" },
                            new Resource { Key = "Users", Value = "Пользователи" },
                            new Resource { Key = "Categories", Value = "Категории" },
                            new Resource { Key = "Submit", Value = "Подтвердить" },
                            new Resource { Key = "Venue", Value = "Площадка" },
                            new Resource { Key = "Buy ticket", Value = "Купить билет" },
                            new Resource { Key = "BY", Value = "БЕЛ" },
                            new Resource { Key = "EN", Value = "АНГЛ" },
                            new Resource { Key = "RU", Value = "РУС" },
                            new Resource { Key = "Privacy policy", Value = "Политика конфиденциальности" },
                            new Resource { Key = "txt1", Value = "Эта политика конфиденциальности была составлена для того, чтобы лучше обслуживать тех, кто обеспокоен тем, как их «Личная информация» (PII) используется в Интернете. PI - это информация, которая может использоваться сама по себе или вместе с другой информацией для идентификации, установления контакта или определения местонахождения отдельного человека или для идентификации человека в контексте. Пожалуйста, внимательно ознакомьтесь с нашей политикой конфиденциальности, чтобы получить четкое представление о том, как мы собираем, используем, защищаем или иным образом обрабатываем вашу Личную информацию в соответствии с нашим веб-сайтом." },
                            new Resource { Key = "txt2", Value = "Наша приверженность конфиденциальности пользователей:" },
                            new Resource { Key = "txt3", Value = "Эта gолитика описывает, как Privacy.com защищает и управляет вашими личными данными, в том числе:" },
                            new Resource { Key = "txt4", Value = "Какие данные мы собираем, когда вы регистрируетесь, используете или взаимодействуете с нашими приложениями, продуктами, услугами или веб-сайтами (совместно именуемые «Услуги»);" },
                            new Resource { Key = "txt5", Value = "Как мы управляем данными с самого начала вашего взаимодействия;" },
                            new Resource { Key = "txt6", Value = "Выбор, который у вас есть в отношении управления данными." },
                            new Resource { Key = "txt7", Value = "Мы ограничиваем использование ваших личных данных:" },
                            new Resource { Key = "txt8", Value = "Для предоставления, поддержки и улучшения наших Услуг;" },
                            new Resource { Key = "txt9", Value = "Чтобы общаться с вами о новых и существующих Услугах;" },
                            new Resource { Key = "txt10", Value = "Для защиты законных прав, собственности и безопасности наших Сервисов и пользователей." },
                            new Resource { Key = "Hello", Value = "Здравствуйте" },
                            new Resource { Key = "Login", Value = "Войти" },
                            new Resource { Key = "Logout", Value = "Выйти" },
                            new Resource { Key = "My orders", Value = "Мои заказы" },
                            new Resource { Key = "My tickets", Value = "Мои билеты" },
                            new Resource { Key = "User info", Value = "Информация о пользователе" },
                            new Resource { Key = "Register", Value = "Регистрация" },
                            new Resource { Key = "Admin panel", Value = "Панель администратора" },
                            new Resource { Key = "Home", Value = "Домашняя" },
                            new Resource { Key = "Edit", Value = "Редактировать" },
                            new Resource { Key = "Delete", Value = "Удалить" },
                            new Resource { Key = "Create City", Value = "Добавить город" },
                            new Resource { Key = "Type here city name", Value = "Введите здесь название города" },
                            new Resource { Key = "Create", Value = "Создать" },
                            new Resource { Key = "Back", Value = "Назад" },
                            new Resource { Key = "Create event", Value = "Добавить событие" },
                            new Resource { Key = "Type here event description", Value = "Введите здесь описание события" },
                            new Resource { Key = "New role", Value = "Новая роль" },
                            new Resource { Key = "Add", Value = "Добавить" },
                            new Resource { Key = "Use a local account to log in.", Value = "Используйте локальную учетную запись для входа в систему." },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
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
                            new Resource { Key = "You come", Value = "Вы патрапілі ў дадатак для онлайн-продажу і перапродажу білетаў на мерапрыемствы. Прыемнага карыстання." },
                            new Resource { Key = "homepagetitle", Value = "Хатняя старонка" },
                            new Resource { Key = "Privacy", Value = "Палітыка прыватнасці" },
                            new Resource { Key = "eventstitle", Value = "Мерапрыемствы" },
                            new Resource { Key = "ordersTitle", Value = "Загады" },
                            new Resource { Key = "ticketsTitle", Value = "Квіткі" },
                            new Resource { Key = "myTicketsTitle", Value = "Мае квіткі" },
                            new Resource { Key = "createTicket", Value = "Стварыць квіток" },
                            new Resource { Key = "login", Value = "Увайсці" },
                            new Resource { Key = "Forgot your password?", Value = "Забыліся свой пароль?" },
                            new Resource { Key = "Register as a new user", Value = "Зарэгістравацца як новы карыстальнік" },
                            new Resource { Key = "Use another service to log in.", Value = "Для ўваходу выкарыстоўвайце іншую службу." },
                            new Resource { Key = "externalAuth", Value = "Знешнія службы аўтэнтыфікацыі не настроены. Глядзіце" },
                            new Resource { Key = "this article", Value = "гэты артыкул" },
                            new Resource { Key = "detailsASP", Value = "для атрымання падрабязнай інфармацыі аб наладжванні гэтага прыкладання ASP.NET для падтрымкі ўваходу праз знешнія службы." },
                            new Resource { Key = "Log in using your", Value = "Увайдзіце, выкарыстоўваючы свой" },
                            new Resource { Key = "account", Value = "акаўнт" },
                            new Resource { Key = "Welcome", Value = "Вітаем" },
                            new Resource { Key = "Cities", Value = "Гарады" },
                            new Resource { Key = "Events", Value = "Падзеі" },
                            new Resource { Key = "Venues", Value = "Пляцоўкі" },
                            new Resource { Key = "Users", Value = "Карыстальнікі" },
                            new Resource { Key = "Categories", Value = "Катэгорыі" },
                            new Resource { Key = "Submit", Value = "Пацвердзіць" },
                            new Resource { Key = "Venue", Value = "Пляцоўка" },
                            new Resource { Key = "Buy ticket", Value = "Купіць білет" },
                            new Resource { Key = "BY", Value = "БЕЛ" },
                            new Resource { Key = "EN", Value = "АНГЛ" },
                            new Resource { Key = "RU", Value = "РУС" },
                            new Resource { Key = "Privacy policy", Value = "Палітыка прыватнасці" },
                            new Resource { Key = "txt1", Value = "Гэтая палітыка прыватнасці была складзена для таго, каб лепш абслугоўваць тых, хто занепакоены тым, як у Інтэрнэце выкарыстоўваецца іх \"Асабістая інфармацыя\". PI - гэта інфармацыя, якая можа выкарыстоўвацца самастойна альбо з іншай інфармацыяй для ідэнтыфікацыі, кантактавання альбо вызначэння месцазнаходжання аднаго чалавека альбо для ідэнтыфікацыі чалавека ў кантэксце. Калі ласка, уважліва прачытайце нашу палітыку прыватнасці, каб атрымаць дакладнае ўяўленне пра тое, як мы збіраем, выкарыстоўваем, абараняем вашу інфармацыю, якая дазваляе ідэнтыфікаваць вашу асобу, у адпаведнасці з нашым веб-сайтам." },
                            new Resource { Key = "txt2", Value = "Наша прыхільнасць прыватнасці карыстальнікаў:" },
                            new Resource { Key = "txt3", Value = "Гэтая палітыка апісвае, як Privacy.com абараняе і кіруе вашымі асабістымі дадзенымі, у тым ліку:" },
                            new Resource { Key = "txt4", Value = "Якія дадзеныя мы збіраем, калі вы падпісваецеся, выкарыстоўваеце альбо ўзаемадзейнічаеце з любымі з нашых прыкладанняў, прадуктаў, паслуг ці вэб-сайтаў (у сукупнасці \"Паслугі\");" },
                            new Resource { Key = "txt5", Value = "Як мы кіруем дадзенымі з самага пачатку вашага ўзаемадзеяння;" },
                            new Resource { Key = "txt6", Value = "У вас ёсць выбар адносна кіравання дадзенымі." },
                            new Resource { Key = "txt7", Value = "Мы абмяжоўваем выкарыстанне вашых асабістых дадзеных:" },
                            new Resource { Key = "txt8", Value = "Дзеля забеспячэння, падтрымання і ўдасканалення нашых Паслуг;" },
                            new Resource { Key = "txt9", Value = "Мець зносіны з вамі пра новыя і існуючыя Паслугі;" },
                            new Resource { Key = "txt10", Value = "Для абароны законных правоў, уласнасці і бяспекі нашых Паслуг і карыстальнікаў." },
                            new Resource { Key = "Hello", Value = "Добры дзень" },
                            new Resource { Key = "Login", Value = "Увайсцi" },
                            new Resource { Key = "Logout", Value = "Выйсцi" },
                            new Resource { Key = "My orders", Value = "Мае заказы" },
                            new Resource { Key = "My tickets", Value = "Мае квіткі" },
                            new Resource { Key = "Register", Value = "Рэгістрацыя" },
                            new Resource { Key = "Admin panel", Value = "Панэль адміністратара" },
                            new Resource { Key = "Home", Value = "Хатняя" },
                            new Resource { Key = "Edit", Value = "Рэдагаваць" },
                            new Resource { Key = "Delete", Value = "Выдаліць" },
                            new Resource { Key = "Create City", Value = "Дадаць горад" },
                            new Resource { Key = "Type here city name", Value = "Увядзіце тут назву горада" },
                            new Resource { Key = "Create", Value = "Стварыць" },
                            new Resource { Key = "Back", Value = "Назад" },
                            new Resource { Key = "Create event", Value = "Стварыць падзею" },
                            new Resource { Key = "Type here event description", Value = "Увядзіце тут апісанне падзеі" },
                            new Resource { Key = "New role", Value = "Новая роля" },
                            new Resource { Key = "Add", Value = "Дадаць" },
                            new Resource { Key = "User info", Value = "Інфармацыя пра карыстальніка" },
                            new Resource { Key = "Use a local account to log in.", Value = "Для ўваходу выкарыстоўвайце лакальны акаўнт." },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                            new Resource { Key = "", Value = "" },
                        }
                    }
                );
                _db.SaveChanges();
            }
            return new EFStringLocalizer(_db);
        }
    }
}
