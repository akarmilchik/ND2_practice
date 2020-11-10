using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface ICitiesService
    {
        Task AddCityToDb(City item);
        Task<List<City>> GetCities();
        Task<City> GetCityById(int id);
        Task RemoveCityFromDb(City item);
        Task UpdCityToDb(City item);
    }
}