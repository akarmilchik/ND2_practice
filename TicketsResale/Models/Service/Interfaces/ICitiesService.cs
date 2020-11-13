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
        Task<City> GetCityById(int id);
        Task<string> GetCityNameById(int id);
        Dictionary<string, int> GetNearPages(List<int> pages, int currentPage);
        Task RemoveCityFromDb(City item);
        Task UpdCityToDb(City item);
    }
}