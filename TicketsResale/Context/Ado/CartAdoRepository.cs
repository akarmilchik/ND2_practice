using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Context.Ado
{
    public class CartAdoRepository : BaseAdoRepository<TicketsCart>
    {
        public CartAdoRepository(IOptions<AdoOptions> options) : base(options)
        {
        }

        public override Task<IEnumerable<TicketsCart>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override async Task<TicketsCart> Get(int id)
        {
            const string cartQuery = "SELECT Id FROM dbo.TicketsCart WHERE Id = @id";
            const string itemsQuery = "SELECT TicketId, Price FROM dbo.CartItem WHERE TicketsCartId = @id";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(cartQuery, new SqlParameter("@id", id));
            TicketsCart result = default;

            if (reader.HasRows)
            {
                await reader.CloseAsync();

                result = new TicketsCart { Id = id, CartItems = new List<CartItem>() };

                reader = await ExecuteQueryAsync(itemsQuery, new SqlParameter("@id", id));
                if (reader.HasRows)
                    while (await reader.ReadAsync())
                        result.CartItems.Add(new CartItem
                        {
                            TicketId = reader.GetInt32(0),
                            Count = reader.GetInt32(1)
                        });
            }
            await reader.CloseAsync();
            await connection.CloseAsync();
            return result;
        }

        public override async Task<int> Add(TicketsCart item)
        {
            const string query = "INSERT INTO dbo.TicketsCart OUTPUT inserted.Id DEFAULT VALUES";
            await connection.OpenAsync();
            var result = await ExecuteScalarAsync(query);
            await connection.CloseAsync();
            return (int)result;
        }

        public override async Task Update(TicketsCart item)
        {
            const string deleteQuery = "DElETE FROM dbo.CartItem WHERE TicketsCartId = @id";
            const string addQuery =
                "INSERT INTO dbo.CartItem(TicketsCartId, TicketId, Count) VALUES (@ticketsCartId, @ticketId, @count)";

            await connection.OpenAsync();
            await ExecuteCommandAsync(deleteQuery, new SqlParameter("@id", item.Id));

            foreach (var cartItem in item.CartItems)
                await ExecuteCommandAsync(addQuery,
                    new SqlParameter("@ticketsCartId", item.Id),
                    new SqlParameter("@ticketId", cartItem.TicketId),
                    new SqlParameter("@count", cartItem.Count));
            await connection.CloseAsync();
        }

        public override async Task Delete(int id)
        {
            await connection.OpenAsync();
            const string query = "DELETE FROM dbo.CartItem WHERE Id = @id";
            await ExecuteCommandAsync(query, new SqlParameter("@id", id));
            await connection.CloseAsync();
        }
    }
}
