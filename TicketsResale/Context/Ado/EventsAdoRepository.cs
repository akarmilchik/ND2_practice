using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Context.Ado
{
    public class EventsAdoRepository : BaseAdoRepository<Event>
    {

        public EventsAdoRepository(IOptions<AdoOptions> options) : base(options)
        {

        }


        public override async Task<IEnumerable<Event>> GetAll()
        {
            const string query = "SELECT Id, Name, Date, VenueId, Banner, Description FROM dbo.Events";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query);

            var categories = new List<Event>();

            if (reader.HasRows)
                while (await reader.ReadAsync())
                    categories.Add(new Event
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Date = reader.GetDateTime(2),
                        VenueId = reader.GetInt32(3),
                        Banner = reader.GetString(4),
                        Description = reader.GetString(5)
                    });
            await reader.CloseAsync();
            await connection.CloseAsync();
            return categories;
        }

        public override async Task<Event> Get(int id)
        {
            const string query = "SELECT Id, Name, Date, VenueId, Banner, Description FROM dbo.Events WHERE Id = @id";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query, new SqlParameter("@id", id));

            Event result = null;
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                result = new Event
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Date = reader.GetDateTime(2),
                    VenueId = reader.GetInt32(3),
                    Banner = reader.GetString(4),
                    Description = reader.GetString(5)
                };
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return result;
        }

        public override async Task<int> Add(Event item)
        {
            const string query =
                "INSERT INTO dbo.Events(Name, Date, VenueId, Banner, Description) OUTPUT Inserted.Id VALUES(@name, @date, @venueId, @banner, @description)";
            await connection.OpenAsync();
            var result = await ExecuteScalarAsync(query,
                new SqlParameter("@name", item.Name),
                new SqlParameter("@date", item.Date),
                new SqlParameter("@venueId", item.VenueId),
                new SqlParameter("@banner", item.Banner),
                new SqlParameter("@description", item.Description)
            );
            await connection.CloseAsync();
            return (int)result;
        }

        public override async Task Update(Event item)
        {
            const string query =
                "UPDATE dbo.Tickets SET Name = @name, Date = @date, VenueId = @venueId, Banner = @banner, Description = @description WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query,
                new SqlParameter("@name", item.Name),
                new SqlParameter("@date", item.Date),
                new SqlParameter("@venueId", item.VenueId),
                new SqlParameter("@banner", item.Banner),
                new SqlParameter("@description", item.Description));
            await connection.CloseAsync();
        }

        public override async Task Delete(int id)
        {
            const string query = "DELETE FROM dbo.Events WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query, new SqlParameter("@id", id));
            await connection.CloseAsync();
        }
    }
}
