using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface IAddDataService
    {
        Task AddCityToDb(City item);
        Task RemoveCityFromDb(City item);
        Task UpdCityToDb(City item);
    }
}