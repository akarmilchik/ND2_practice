using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface ICitiesService
    {
        Task AddCityToDb(City item);
        Task<List<City>> GetCities(int page, int pageSize);
        Task<List<City>> GetCities();
        List<int> GetCitiesPages(int pageSize);
        IQueryable<City> GetCitiesSimple();
        Task<City> GetCityById(int id);
        Task<string> GetCityNameById(int id);
        Task RemoveCityFromDb(City item);
        Task UpdCityToDb(City item);
    }
}