using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Models.Service
{
    public class CitiesService : ICitiesService
    {
        private readonly StoreContext context;

        public CitiesService(StoreContext storeContext)
        {
            this.context = storeContext;
        }

        public async Task<List<City>> GetCities()
        {
            return await context.Cities.ToListAsync();
        }

        public async Task<List<City>> GetCities(int page, int pageSize)
        {
            if (pageSize <= 0)
                pageSize = 10;

            if (page <= 0)
                page = 1;

            var needCities = ((await context.Cities.ToListAsync()).Skip(pageSize * (page - 1)).Take(pageSize)).ToList();

            return needCities;
        }


        public IQueryable<City> GetCitiesQuery()
        {
            var needCities = context.Cities.OrderBy(c => c.Name);

            return needCities;
        }

        public List<int> GetCitiesPages(int pageSize)
        { 
            var countCities = context.Cities.Count();

            if (pageSize == 0)
                pageSize = 1;

            var res = (countCities / pageSize) + 1;

            List<int> result = new List<int>();

            for(int i = 1; i <= res; i++)
            { 
                result.Add(i); 
            }

            return result;
        }

        public Dictionary<string, int> GetNearPages(List<int> pages, int currentPage)
        {
            Dictionary<string, int> nearPages = new Dictionary<string, int>();

            if (currentPage == 0)
                currentPage = 1;


            if (currentPage > 1 && currentPage < pages.Count())
            {
                nearPages.Add("prevPage", currentPage - 1);
                nearPages.Add("nextPage", currentPage + 1);
            }
            else if (currentPage == 1)
            {
                nearPages.Add("prevPage", currentPage);
                nearPages.Add("nextPage", currentPage + 1);
            }
            else
            {
                nearPages.Add("prevPage", currentPage - 1);
                nearPages.Add("nextPage", currentPage);
            }
            return nearPages;
        }

        public async Task<City> GetCityById(int id)
        {
            return await context.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> GetCityNameById(int id)
        {
            return await context.Cities.Where(c => c.Id == id).Select(c => c.Name).FirstOrDefaultAsync();
        }


        public async Task AddCityToDb(City item)
        {
            context.Database.EnsureCreated();

            await context.Cities.AddAsync(item);

            await context.SaveChangesAsync();
        }

        public async Task UpdCityToDb(City item)
        {
            context.Database.EnsureCreated();

            context.Cities.Update(item);

            await context.SaveChangesAsync();
        }

        public async Task RemoveCityFromDb(City item)
        {
            context.Database.EnsureCreated();

            context.Cities.Remove(item);


            await context.SaveChangesAsync();
        }
    }
}
