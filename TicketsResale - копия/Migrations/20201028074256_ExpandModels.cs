using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsResale.Migrations
{
    public partial class ExpandModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SellerId1",
                table: "Tickets",
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
                name: "IX_Tickets_SellerId1",
                table: "Tickets",
                column: "SellerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId1",
                table: "Tickets",
                column: "SellerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SellerId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SellerId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SellerId1",
                table: "Tickets");

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
