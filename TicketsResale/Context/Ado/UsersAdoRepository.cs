using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Context.Ado
{
    public class UsersAdoRepository : BaseAdoRepository<User>
    {

        public UsersAdoRepository(IOptions<AdoOptions> options) : base(options)
        {

        }


        public override async Task<IEnumerable<User>> GetAll()
        {
            const string query = "SELECT Id, FirstName, LastName, Localization, Address, PhoneNumber, UserName, Password, Role FROM dbo.Users";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query);

            var categories = new List<User>();

            if (reader.HasRows)
                while (await reader.ReadAsync())
                    categories.Add(new User
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Localization = reader.GetString(3),
                        Address = reader.GetString(4),
                        PhoneNumber = reader.GetString(5),
                        UserName = reader.GetString(6),
                        Password = reader.GetString(7),
                        Role = reader.GetString(8)
                    });
            await reader.CloseAsync();
            await connection.CloseAsync();
            return categories;
        }

        public override async Task<User> Get(int id)
        {
            const string query = "SELECT Id, FirstName, LastName, Localization, Address, PhoneNumber, UserName, Password, Role FROM dbo.Users WHERE Id = @id";
            await connection.OpenAsync();
            var reader = await ExecuteQueryAsync(query, new SqlParameter("@id", id));

            User result = null;
            if (reader.HasRows)
            {
                await reader.ReadAsync();
                result = new User
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Localization = reader.GetString(3),
                    Address = reader.GetString(4),
                    PhoneNumber = reader.GetString(5),
                    UserName = reader.GetString(6),
                    Password = reader.GetString(7),
                    Role = reader.GetString(8)
                };
            }

            await reader.CloseAsync();
            await connection.CloseAsync();
            return result;
        }

        public override async Task<int> Add(User item)
        {
            const string query =
                "INSERT INTO dbo.Users(FirstName, LastName, Localization, Address, PhoneNumber, UserName, Password, Role) " +
                "OUTPUT Inserted.Id VALUES(@firstName, @lastName, @localization, @address, @phoneNumber, @userName, @password, @role)";
            await connection.OpenAsync();
            var result = await ExecuteScalarAsync(query,
                new SqlParameter("@firstName", item.FirstName),
                new SqlParameter("@lastName", item.LastName),
                new SqlParameter("@localization", item.Localization),
                new SqlParameter("@address", item.Address),
                new SqlParameter("@phoneNumber", item.PhoneNumber),
                new SqlParameter("@userName", item.UserName),
                new SqlParameter("@password", item.Password),
                new SqlParameter("@role", item.Role)
            );
            await connection.CloseAsync();
            return (int)result;
        }

        public override async Task Update(User item)
        {
            const string query =
                "UPDATE dbo.Users SET FirstName = @firstName, LastName = @lastName, Localization = @localization, Address = @address, " +
                "PhoneNumber = @phoneNumber, UserName = @userName, Password = @password, Role = @role,  WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query,
                new SqlParameter("@firstName", item.FirstName),
                new SqlParameter("@lastName", item.LastName),
                new SqlParameter("@localization", item.Localization),
                new SqlParameter("@address", item.Address),
                new SqlParameter("@phoneNumber", item.PhoneNumber),
                new SqlParameter("@userName", item.UserName),
                new SqlParameter("@password", item.Password),
                new SqlParameter("@role", item.Role)
                );
            await connection.CloseAsync();
        }

        public override async Task Delete(int id)
        {
            const string query = "DELETE FROM dbo.Users WHERE Id = @id";
            await connection.OpenAsync();
            await ExecuteCommandAsync(query, new SqlParameter("@id", id));
            await connection.CloseAsync();
        }
    }
}
