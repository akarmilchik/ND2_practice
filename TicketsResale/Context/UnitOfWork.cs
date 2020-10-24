using System;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context.Ado;

namespace TicketsResale.Context
{
    public class UnitOfWork : IDisposable
    {
        public UnitOfWork(string basePath)
        {
            CitiesRepository = new GenericFileRepository<City>(basePath);
            VenuesRepository = new GenericFileRepository<Venue>(basePath);
            EventsRepository = new GenericFileRepository<Event>(basePath);
            TicketsRepository = new GenericFileRepository<Ticket>(basePath);
            OrdersRepository = new GenericFileRepository<Order>(basePath);
            TicketsCartsRepository = new GenericFileRepository<TicketsCart>(basePath);
        }

        public IRepository<City> CitiesRepository { get; private set; }
        public IRepository<Venue> VenuesRepository { get; private set; }
        public IRepository<Event> EventsRepository { get; private set; }
        public IRepository<Ticket> TicketsRepository { get; private set; }
        public IRepository<Order> OrdersRepository { get; private set; }
        public IRepository<TicketsCart> TicketsCartsRepository { get; private set; }

        public async Task SaveAsync()
        {
            await CitiesRepository.SaveChangesAsync();
            await VenuesRepository.SaveChangesAsync();
            await EventsRepository.SaveChangesAsync();
            await TicketsRepository.SaveChangesAsync();
            await OrdersRepository.SaveChangesAsync();
            await TicketsCartsRepository.SaveChangesAsync();
        }
        public void Dispose()
        {
            CitiesRepository.Dispose();
            VenuesRepository.Dispose();
            EventsRepository.Dispose();
            TicketsRepository.Dispose();
            OrdersRepository.Dispose();
            TicketsCartsRepository.Dispose();
        }

    }
}
