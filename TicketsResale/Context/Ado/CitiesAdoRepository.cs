using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Context.Ado
{
    public class CitiesAdoRepository : BaseAdoRepository<City>
    {
        public CitiesAdoRepository(IOptions<AdoOptions> options) : base(options)
        {

        }


        public override async Task<IEnumerable<City>> GetAll()
        {
            const string query = "SELECT Id, Name FROM dbo.Cities";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query);

            var categories = new List<City>();

            if (reader.HasRows)
                while (await reader.ReadAsync())
                    categories.Add(new City
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
            await reader.CloseAsync();
            await connection.CloseAsync();
            return categories;
        }

        public override async Task<City> Get(int id)
        {
            const string query = "SELECT Id, Name FROM dbo.Cities WHERE Id = @id";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query, new SqlParameter("@id", id));

            City result = null;
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                result = new City
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return result;
        }

        public override async Task<int> Add(City item)
        {
            const string query =
                "INSERT INTO dbo.Tickets(Name) OUTPUT Inserted.Id VALUES(@name)";
            await connection.OpenAsync();
            var result = await ExecuteScalarAsync(query,
                new SqlParameter("@name", item.Name)
            );
            await connection.CloseAsync();
            return (int)result;
        }

        public override async Task Update(City item)
        {
            const string query =
                "UPDATE dbo.Tickets SET Name = @name WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query,
                new SqlParameter("@name", item.Name)
                );
            await connection.CloseAsync();
        }

        public override async Task Delete(int id)
        {
            const string query = "DELETE FROM dbo.Cities WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query, new SqlParameter("@id", id));
            await connection.CloseAsync();
        }


    }
}
