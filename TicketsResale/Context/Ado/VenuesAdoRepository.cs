using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsResale.Context.Ado
{
    public class VenuesAdoRepository
    {

        public TicketsAdoRepository(IOptions<AdoOptions> options) : base(options)
        {

        }


        public override async Task<IEnumerable<Ticket>> GetAll()
        {
            const string query = "SELECT Id, EventId, SellerId, Price, Status FROM dbo.Tickets";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query);

            var categories = new List<Ticket>();

            if (reader.HasRows)
                while (await reader.ReadAsync())
                    categories.Add(new Ticket
                    {
                        Id = reader.GetInt32(0),
                        EventId = reader.GetInt32(1),
                        SellerId = reader.GetInt32(2),
                        Price = reader.GetDecimal(3),
                        Status = reader.GetString(4),
                    });
            await reader.CloseAsync();
            await connection.CloseAsync();
            return categories;
        }

        public override async Task<Ticket> Get(int id)
        {
            const string query = "SELECT Id, EventId, SellerId, Price, Status FROM dbo.Tickets WHERE Id = @id";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query, new SqlParameter("@id", id));

            Ticket result = null;
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                result = new Ticket
                {
                    Id = reader.GetInt32(0),
                    EventId = reader.GetInt32(1),
                    SellerId = reader.GetInt32(2),
                    Price = reader.GetDecimal(3),
                    Status = reader.GetString(4),
                };
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return result;
        }

        public override async Task<int> Add(Ticket item)
        {
            const string query =
                "INSERT INTO dbo.Tickets(EventId, SellerId, Price, Status) OUTPUT Inserted.Id VALUES(@eventId, @sellerId, @price, @status)";
            await connection.OpenAsync();
            var result = await ExecuteScalarAsync(query,
                new SqlParameter("@eventId", item.EventId),
                new SqlParameter("@sellerId", item.SellerId),
                new SqlParameter("@price", item.Price),
                new SqlParameter("@status", item.Status)
            );
            await connection.CloseAsync();
            return (int)result;
        }

        public override async Task Update(Ticket item)
        {
            const string query =
                "UPDATE dbo.Tickets SET EventId = @eventId, SellerId = @sellerId, Price = @price, Status = @status  WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query,
                new SqlParameter("@eventId", item.EventId),
                new SqlParameter("@sellerId", item.SellerId),
                new SqlParameter("@price", item.Price),
                new SqlParameter("@status", item.Status),
                new SqlParameter("@id", item.Id));
            await connection.CloseAsync();
        }

        public override async Task Delete(int id)
        {
            const string query = "DELETE FROM dbo.Tickets WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query, new SqlParameter("@id", id));
            await connection.CloseAsync();
        }
    }
}
