using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsResale.Migrations
{
    public partial class Identity_ChangeModels_Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                table: "CartItems",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "CartItems",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "TrackNumber",
                table: "CartItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Localization",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SellerId",
                table: "Tickets",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_BuyerId",
                table: "CartItems",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AspNetUsers_BuyerId",
                table: "CartItems",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId",
                table: "Tickets",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AspNetUsers_BuyerId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SellerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_BuyerId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "TrackNumber",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Localization",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    TrackNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TicketId",
                table: "Orders",
                column: "TicketId");
        }
    }
}
