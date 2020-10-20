using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Context.Ado
{
    public class VenuesAdoRepository : BaseAdoRepository<Venue>
    {

        public VenuesAdoRepository(IOptions<AdoOptions> options) : base(options)
        {

        }


        public override async Task<IEnumerable<Venue>> GetAll()
        {
            const string query = "SELECT Id, Name, Address, CityId FROM dbo.Venues";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query);

            var categories = new List<Venue>();

            if (reader.HasRows)
                while (await reader.ReadAsync())
                    categories.Add(new Venue
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        CityId = reader.GetInt32(3)
                    });
            await reader.CloseAsync();
            await connection.CloseAsync();
            return categories;
        }

        public override async Task<Venue> Get(int id)
        {
            const string query = "SELECT Id, Name, Address, CityId FROM dbo.Venues WHERE Id = @id";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query, new SqlParameter("@id", id));

            Venue result = null;
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                result = new Venue
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Address = reader.GetString(2),
                    CityId = reader.GetInt32(3)
                };
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return result;
        }

        public override async Task<int> Add(Venue item)
        {
            const string query =
                "INSERT INTO dbo.Venues(Name, Address, CityId) OUTPUT Inserted.Id VALUES(@name, @address, @cityId)";
            await connection.OpenAsync();
            var result = await ExecuteScalarAsync(query,
                new SqlParameter("@name", item.Name),
                new SqlParameter("@address", item.Address),
                new SqlParameter("@cityId", item.CityId)
            );
            await connection.CloseAsync();
            return (int)result;
        }

        public override async Task Update(Venue item)
        {
            const string query =
                "UPDATE dbo.Venues SET Name = @name, Address = @address, CityId = @cityId WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query,
                new SqlParameter("@name", item.Name),
                new SqlParameter("@address", item.Address),
                new SqlParameter("@cityId", item.CityId)
                );
            await connection.CloseAsync();
        }

        public override async Task Delete(int id)
        {
            const string query = "DELETE FROM dbo.Venues WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query, new SqlParameter("@id", id));
            await connection.CloseAsync();
        }
    }
}
