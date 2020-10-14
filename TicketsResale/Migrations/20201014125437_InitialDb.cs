using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsResale.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Grodno" },
                    { 2, "Minsk" },
                    { 3, "Barselona" },
                    { 4, "NewYork" },
                    { 5, "Tokyo" },
                    { 6, "Dubai" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "FirstName", "LastName", "Localization", "Password", "PhoneNumber", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "adrUser0", "Alexey", "Robinson", "rus", "admin", "228228", "Administrator", "admin" },
                    { 2, "adrUser1", "Jominez", "Maxwell", "spa", "user", "345124", "User", "user" },
                    { 3, "adrUser2", "Alibaba", "Bestseller", "eng", "seller", "777777", "User", "seller" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
