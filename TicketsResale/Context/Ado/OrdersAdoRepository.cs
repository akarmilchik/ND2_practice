using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Context.Ado
{
    public class OrdersAdoRepository : BaseAdoRepository<Order>
    {

        public OrdersAdoRepository(IOptions<AdoOptions> options) : base(options)
        {

        }

        public override async Task<IEnumerable<Order>> GetAll()
        {
            const string query = "SELECT Id, TicketId, Status, BuyerId, TrackNumber FROM dbo.Orders";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query);

            var categories = new List<Order>();

            if (reader.HasRows)
                while (await reader.ReadAsync())
                    categories.Add(new Order
                    {
                        Id = reader.GetInt32(0),
                        TicketId = reader.GetInt32(1),
                        Status = reader.GetString(2),
                        BuyerId = reader.GetInt32(3),
                        TrackNumber = reader.GetString(4)
                    });
            await reader.CloseAsync();
            await connection.CloseAsync();
            return categories;
        }

        public override async Task<Order> Get(int id)
        {
            const string query = "SELECT Id, TicketId, Status, BuyerId, TrackNumber FROM dbo.Orders WHERE Id = @id";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query, new SqlParameter("@id", id));

            Order result = null;
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                result = new Order
                {
                    Id = reader.GetInt32(0),
                    TicketId = reader.GetInt32(1),
                    Status = reader.GetString(2),
                    BuyerId = reader.GetInt32(3),
                    TrackNumber = reader.GetString(4)
                };
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return result;
        }

        public override async Task<int> Add(Order item)
        {
            const string query =
                "INSERT INTO dbo.Orders(TicketId, Status, BuyerId, TrackNumber) OUTPUT Inserted.Id VALUES(@ticketId, @status, @buyerId, @trackNumber)";
            await connection.OpenAsync();
            var result = await ExecuteScalarAsync(query,
                new SqlParameter("@ticketId", item.TicketId),
                new SqlParameter("@status", item.Status),
                new SqlParameter("@buyerId", item.BuyerId),
                new SqlParameter("@trackNumber", item.TrackNumber)
            );
            await connection.CloseAsync();
            return (int)result;
        }

        public override async Task Update(Order item)
        {
            const string query =
                "UPDATE dbo.Orders SET TicketId = @ticketId, Status = @status, BuyerId = @buyerId, TrackNumber = @trackNumber  WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query,
                new SqlParameter("@ticketId", item.TicketId),
                new SqlParameter("@status", item.Status),
                new SqlParameter("@buyerId", item.BuyerId),
                new SqlParameter("@trackNumber", item.TrackNumber),
                new SqlParameter("@id", item.Id));
            await connection.CloseAsync();
        }

        public override async Task Delete(int id)
        {
            const string query = "DELETE FROM dbo.Orders WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query, new SqlParameter("@id", id));
            await connection.CloseAsync();
        }
    }
}
